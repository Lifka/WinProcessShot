using System.Diagnostics;
using Microsoft.O365.Security.ETW;
using WinProcessShot.Model;

namespace WinProcessShot.Controller
{
    internal static class ETWMonitorMode
    {
        #region MEMBERS

        private static bool monitorModeActive = false;
        private static UserTrace traceSession = null;

        public static ProcessShotResults? Results { get; set; } = null;
        public static bool EnableAutomaticDump { get; set; } = false;
        public static bool EnableAutomaticExeBackup { get; set; } = false;

        #endregion

        #region METHODS
        public static void StartMonitorMode()
        {
            LoadOptions();
            Results = new ProcessShotResults();

            Results.Mode = MODE.MONITOR_MODE;
            monitorModeActive = true;

            new Task(() => StartMonitorModeTask()).Start();
        }

        private static void StartMonitorModeTask()
        {
            traceSession = new UserTrace("WinProcessShot-ETWMonitorMode");
            traceSession.Enable(GetProcessETWProvider());
            traceSession.Start();
        }

        public static void StopMonitorMode()
        {
            monitorModeActive = false;
            if (traceSession == null)
            {
                return;
            }

            traceSession.Stop();
            traceSession.Dispose();
        }

        internal static void Clear()
        {
            StopMonitorMode();
            if (Results != null)
            {
                Results.ResultReady = false;
                Results.Dispose();
                Results = null;
            }
        }

        private static Provider GetProcessETWProvider()
        {
            /*
             * https://github.com/repnz/etw-providers-docs/blob/d5f68e8acda5da154ab44e405b610dd8c2ba1164/Manifests-Win10-18990/Microsoft-Windows-Kernel-Process.xml#L4
            */

            Provider provider = new Provider("Microsoft-Windows-Kernel-Process");

            var filter = new EventFilter(
                Microsoft.O365.Security.ETW.Filter.EventIdIs(1)
                .Or(Microsoft.O365.Security.ETW.Filter.EventIdIs(2))
            );
            filter.OnEvent += ETWCallBack;

            provider.AddFilter(filter);
            return provider;
        }

        private static void ETWCallBack(IEventRecord eventRecord)
        {

            switch (eventRecord.Id)
            {
                case 1:
                    {
                        AddNewProcess(eventRecord);
                        break;
                    }
                case 2:
                    {
                        AddFinishedProcess(eventRecord);
                        break;
                    }
            }

        }

        private static void AddNewProcess(IEventRecord eventRecord)
        {
            ProcessInfoObj processInfoObj = GetProcessInfoObjFromEventRecordStartProcess(eventRecord);

            if (processInfoObj == null || Results == null 
                || !FiltersController.PassFilters(processInfoObj))
            {
                return;
            }

            if (EnableAutomaticDump)
            {
                if (processInfoObj.Name != null 
                    && !processInfoObj.Name.Contains("ConHost", StringComparison.InvariantCultureIgnoreCase))
                {
                    DumpGenerator.WriteDumpForProcessAsync(processInfoObj.PID, processInfoObj.GetProcessKey());
                }
            }

            if (EnableAutomaticExeBackup)
            {
                ExecutableBackupController.CreateExecutableBackupForProcessAsync(processInfoObj.ExecutablePath, processInfoObj.GetProcessKey());
            }

            if (Results.NewProcessesKeys.Contains(processInfoObj.GetProcessKey()))
            {
                return;
            }

            Results.AddNewProcess(processInfoObj);
            Results.ResultReady = true;
        }

        private static void AddFinishedProcess(IEventRecord eventRecord)
        {
            ProcessInfoObj processInfoObj = GetProcessInfoObjFromEventRecordStopProcess(eventRecord);

            if (processInfoObj == null || Results == null 
                || !FiltersController.PassFilters(processInfoObj))
            {
                return;
            }

            if (Results.FinishedProcessesKeys.Contains(processInfoObj.GetProcessKey()))
            {
                return;
            }

            Results.AddFinishedProcess(processInfoObj);
            Results.ResultReady = true;
        }

        private static ProcessInfoObj GetProcessInfoObjFromEventRecordStopProcess(IEventRecord eventRecord)
        {
            /*
             * https://github.com/repnz/etw-providers-docs/blob/d5f68e8acda5da154ab44e405b610dd8c2ba1164/Manifests-Win10-18990/Microsoft-Windows-Kernel-Process.xml#L4
            */

            ProcessInfoObj processInfoObj = new ProcessInfoObj();
            processInfoObj.Name = Uri.UnescapeDataString(eventRecord.GetAnsiString("ImageName"));
            processInfoObj.PID = eventRecord.GetInt32("ProcessID");

            string key = processInfoObj.GetProcessKey();
            if (Results.KnownProcesses.ContainsKey(key))
            {
                processInfoObj = Results.KnownProcesses[key];
            }
            processInfoObj.ExitCode = eventRecord.GetUInt32("ExitCode");

            return processInfoObj;
        }

        private static ProcessInfoObj GetProcessInfoObjFromEventRecordStartProcess(IEventRecord eventRecord)
        {
            /*
             * https://github.com/repnz/etw-providers-docs/blob/d5f68e8acda5da154ab44e405b610dd8c2ba1164/Manifests-Win10-18990/Microsoft-Windows-Kernel-Process.xml#L4
            */

            ProcessInfoObj processInfoObj = new ProcessInfoObj();
            processInfoObj.PID = eventRecord.GetInt32("ProcessID");

            try
            {
                processInfoObj.ExecutablePath = DevicePathMapper.FromDevicePath(Uri.UnescapeDataString(eventRecord.GetUnicodeString("ImageName")));
                if (!string.IsNullOrEmpty(processInfoObj.ExecutablePath))
                {
                    processInfoObj.Name = Path.GetFileName(processInfoObj.ExecutablePath);
                    processInfoObj.CompanyName = FileVersionInfo.GetVersionInfo(processInfoObj.ExecutablePath).CompanyName;
                    processInfoObj.InternalName = FileVersionInfo.GetVersionInfo(processInfoObj.ExecutablePath).InternalName;
                }
            }
            catch { }

            processInfoObj.CalculateMD5();
            processInfoObj.Trusted = TrustedVerifier.IsTrusted(processInfoObj);
            GetProcInfoFromProcessesTree(ref processInfoObj);
            processInfoObj.LoadCommandLine();
            processInfoObj.LoadParentProcess();

            return processInfoObj;
        }

        private static void GetProcInfoFromProcessesTree(ref ProcessInfoObj processInfoObj)
        {
            try
            {
                Process proc = Process.GetProcessById(processInfoObj.PID);
                processInfoObj.BasePriority = proc.BasePriority;
                if (proc.MainModule != null)
                {
                    processInfoObj.ExecutablePath = proc.MainModule.FileName;
                    processInfoObj.InternalName = proc.MainModule.FileVersionInfo.InternalName;
                    processInfoObj.FileName = proc.MainModule.FileVersionInfo.FileName;
                    processInfoObj.CompanyName = proc.MainModule.FileVersionInfo.CompanyName;
                    processInfoObj.EntryPointAddress = proc.MainModule.EntryPointAddress;
                    processInfoObj.BaseAddress = proc.MainModule.BaseAddress;
                    processInfoObj.ModuleMemorySize = proc.MainModule.ModuleMemorySize;
                }
            }
            catch {}
        }

        public static void RemoveResultsByName(string name)
        {
            if (Results == null)
            {
                return;
            }

            Results.RemoveResultsByName(name);
        }

        public static void RemoveResultsByInternalName(string internalName)
        {
            if (Results == null)
            {
                return;
            }

            Results.RemoveResultsByInternalName(internalName);
        }

        public static void RemoveResultsByExecutablePath(string executablePath)
        {
            if (Results == null)
            {
                return;
            }

            Results.RemoveResultsByExecutablePath(executablePath);
        }

        public static void RemoveResultsByPID(int PID)
        {
            if (Results == null)
            {
                return;
            }

            Results.RemoveResultsByPID(PID);
        }

        private static void LoadOptions()
        {
            if (Properties.Settings.Default == null)
            {
                return;
            }

            EnableAutomaticDump = Properties.Settings.Default.EnableAutomaticDump;
            EnableAutomaticExeBackup = Properties.Settings.Default.EnableAutomaticExeBackup;
        }

        #endregion
    }
}