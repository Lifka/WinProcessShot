using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinProcessShot.Model;

namespace WinProcessShot.Controller
{
    internal static class ProcessDifferencesShot
    {
        #region MEMBERS

        private static Shot? firstShot = null;
        private static Shot? lastShot = null;

        public static ProcessShotResults? Results { get; set; } = null;
        public static bool EnableAutomaticDump { get; set; } = false;
        public static bool EnableAutomaticExeBackup { get; set; } = false;

        #endregion

        #region METHODS
        internal static void TakeFirstShot()
        {
            firstShot = new Shot();
        }

        internal static void TakeLastShot()
        {
            if (firstShot == null)
            {
                throw new Exception("It's mandatory to take first the first shot");
            }

            lastShot = new Shot();
        }

        internal static void Clear()
        {
            firstShot = null;
            lastShot = null;
            Results = null;
        }

        internal static void GenerateShotResults()
        {
            if (firstShot == null || lastShot == null)
            {
                return;
            }

            LoadOptions();
            Results = GenerateResults(firstShot, lastShot, null, EnableAutomaticDump, EnableAutomaticExeBackup);
            Results.Mode = MODE.SHOT;
        }

        internal static ProcessShotResults GenerateResults(Shot? firstShot, Shot? lastShot, List<Shot> previousShot = null, bool createDump = false, bool createExecutableBackup = false)
        {
            if (firstShot == null || lastShot == null)
            {
                return null;
            }

            ProcessShotResults results = new ProcessShotResults();

            results.FirstShotTime = firstShot.TimeStamp; 
            results.LastShotTime = lastShot.TimeStamp;

            results.NewProcesses = new List<ProcessInfoObj>();
            results.FinishedProcesses = new List<ProcessInfoObj>();
            results.ChangedProcesses = new List<ChangedProcess>();

            foreach (int pid in firstShot.ProcessesByPid.Keys)
            {
                // Same process?
                if (lastShot.ProcessesByPid.Keys.Contains(pid) && IsTheSameProcess(firstShot.ProcessesByPid[pid], lastShot.ProcessesByPid[pid]))
                {
                    // Process has changed?
                    if (ProcessHasChanged(firstShot.ProcessesByPid[pid], lastShot.ProcessesByPid[pid]))
                    {
                        if (!FiltersController.PassFilters(firstShot.ProcessesByPid[pid]))
                        {
                            continue;
                        }
                        ChangedProcess changedProcess = new ChangedProcess();
                        changedProcess.Before = firstShot.ProcessesByPid[pid];
                        changedProcess.After = lastShot.ProcessesByPid[pid];
                        ((List<ChangedProcess>)results.ChangedProcesses).Add(changedProcess);
                    }

                    continue;
                }

                if (!FiltersController.PassFilters(firstShot.ProcessesByPid[pid]))
                {
                    continue;
                }

                AddUniqueFinishedProcessToResults(results, firstShot.ProcessesByPid[pid], previousShot);
            }

            foreach (int pid in lastShot.ProcessesByPid.Keys)
            {
                if (firstShot.ProcessesByPid.Keys.Contains(pid) 
                    && IsTheSameProcess(lastShot.ProcessesByPid[pid], firstShot.ProcessesByPid[pid]))
                {
                    continue;
                }

                if (!FiltersController.PassFilters(lastShot.ProcessesByPid[pid]))
                {
                    continue;
                }
                AddUniqueNewProcessToResults(results, lastShot.ProcessesByPid[pid], previousShot, createDump, createExecutableBackup);
            }

            results.ResultReady = true;

            return results;
        }

        private static void AddUniqueNewProcessToResults(ProcessShotResults results, ProcessInfoObj processInfoObj, List<Shot> previousShot = null, bool createDump = false, bool createExecutableBackup = false)
        {
            string key = processInfoObj.GetProcessKey();
            if (results.NewProcessesKeys.Contains(key))
            {
                return;
            }

            if (processInfoObj.ExecutablePath == null
                && previousShot != null && previousShot.Any()
                && TryToGetNotNullProcessInfoObjFromPreviousShot(previousShot, processInfoObj.PID, key, out ProcessInfoObj processInfoObjNotNull))
            {
                processInfoObj = processInfoObjNotNull;
            }

            processInfoObj.CalculateMD5();
            processInfoObj.Trusted = TrustedVerifier.IsTrusted(processInfoObj);
            processInfoObj.LoadCommandLine();
            processInfoObj.LoadParentProcess();
            if (createDump)
            {
                if (!processInfoObj.InternalName.Equals("ConHost")
                    && !processInfoObj.Name.Contains("ConHost", StringComparison.InvariantCultureIgnoreCase))
                {
                    DumpGenerator.WriteDumpForProcessAsync(processInfoObj.PID, processInfoObj.GetProcessKey());
                }
            }

            if (createExecutableBackup)
            {
                ExecutableBackupController.CreateExecutableBackupForProcessAsync(processInfoObj.ExecutablePath, processInfoObj.GetProcessKey());
            }

            results.NewProcessesKeys.Add(key);
            ((List<ProcessInfoObj>)results.NewProcesses).Add(processInfoObj);
        }

        private static void AddUniqueFinishedProcessToResults(ProcessShotResults results, ProcessInfoObj processInfoObj, List<Shot> previousShot = null)
        {
            string key = processInfoObj.GetProcessKey();
            if (results.FinishedProcessesKeys.Contains(key))
            {
                return;
            }

            if (processInfoObj.ExecutablePath == null
                && previousShot != null && previousShot.Any()
                && TryToGetNotNullProcessInfoObjFromPreviousShot(previousShot, processInfoObj.PID, key, out ProcessInfoObj processInfoObjNotNull))
            {
                processInfoObj = processInfoObjNotNull;
            }

            processInfoObj.CalculateMD5();
            processInfoObj.Trusted = TrustedVerifier.IsTrusted(processInfoObj);
            processInfoObj.LoadCommandLine();
            processInfoObj.LoadParentProcess();

            results.FinishedProcessesKeys.Add(key);
            ((List<ProcessInfoObj>)results.FinishedProcesses).Add(processInfoObj);
        }

        private static bool TryToGetNotNullProcessInfoObjFromPreviousShot(List<Shot> previousShot, int pid, string key, out ProcessInfoObj processInfoObjNotNull)
        {
            bool result = false;
            processInfoObjNotNull = null;

            Shot lastShot = previousShot[^1];
            if (lastShot.ProcessesByPid.ContainsKey(pid)
                && lastShot.ProcessesByPid[pid].GetProcessKey().Equals(key)
                && lastShot.ProcessesByPid[pid].ExecutablePath != null)
            {
                result = true;
                processInfoObjNotNull = lastShot.ProcessesByPid[pid];
            }

            return result;
        }

        private static bool PassFilters(ProcessInfoObj process, IEnumerable<Filter> filters)
        {
            if (filters == null)
            {
                return true;
            }

            foreach(Filter filter in filters) 
            {
                if (filter.MatchFilter(process))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsTheSameProcess(ProcessInfoObj firstProcess, ProcessInfoObj lastProcess)
        { 
            bool sameProcess = true;

            if (firstProcess.PID != lastProcess.PID)
            { 
                return false;
            }

            if (firstProcess.Name != lastProcess.Name)
            {
                return false;
            }

            return sameProcess;
        }

        private static bool ProcessHasChanged(ProcessInfoObj firstProcess, ProcessInfoObj lastProcess)
        {
            bool processHasChanged = false;

            if (firstProcess.ExecutablePath != null && firstProcess.ExecutablePath != null)
            {
                if ((firstProcess.ExecutablePath != null && lastProcess.ExecutablePath == null)
                    || (firstProcess.ExecutablePath == null && lastProcess.ExecutablePath != null))
                {
                    return true;
                }

                if (firstProcess.ExecutablePath != lastProcess.ExecutablePath)
                {
                    return true;
                }
            }

            if (firstProcess.InternalName != null && lastProcess.InternalName != null)
            {
                if ((firstProcess.InternalName != null && lastProcess.InternalName == null)
                    || (firstProcess.InternalName == null && lastProcess.InternalName != null))
                {
                    return true;
                }

                if (firstProcess.InternalName != lastProcess.InternalName)
                {
                    return true;
                }
            }

            if (firstProcess.BaseAddress != null && lastProcess.BaseAddress != null)
            {
                if ((firstProcess.BaseAddress != null && lastProcess.BaseAddress == null)
                    || (firstProcess.BaseAddress == null && lastProcess.BaseAddress != null))
                {
                    return true;
                }

                if (firstProcess.BaseAddress != lastProcess.BaseAddress)
                {
                    return true;
                }
            }

            if (firstProcess.EntryPointAddress != null && lastProcess.EntryPointAddress != null)
            {
                if ((firstProcess.EntryPointAddress != null && lastProcess.EntryPointAddress == null)
                    || (firstProcess.EntryPointAddress == null && lastProcess.EntryPointAddress != null))
                {
                    return true;
                }

                if (firstProcess.EntryPointAddress != lastProcess.EntryPointAddress)
                {
                    return true;
                }
            }

            if (firstProcess.ModuleMemorySize != null && lastProcess.ModuleMemorySize != null)
            {
                if ((firstProcess.ModuleMemorySize != null && lastProcess.ModuleMemorySize == null)
                    || (firstProcess.ModuleMemorySize == null && lastProcess.ModuleMemorySize != null))
                {
                    return true;
                }

                if (firstProcess.ModuleMemorySize != lastProcess.ModuleMemorySize)
                {
                    return true;
                }
            }

            return processHasChanged;
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
