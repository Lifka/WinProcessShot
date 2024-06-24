using System.Diagnostics;
using System.Management;
using WinProcessShot.Model;

namespace WinProcessShot.Controller
{
    internal static class ProcessesInfo
    {

        #region MEMBERS


        #endregion

        #region METHODS

        public static Dictionary<int, ProcessInfoObj> GetCurrentProcessesInfoByPid()
        {
            Dictionary<int, ProcessInfoObj> processesInfoByPid = new Dictionary<int, ProcessInfoObj>();

            Process[] localHostProcesses = Process.GetProcesses();

            foreach (Process localHostProcess in localHostProcesses) 
            { 
                ProcessInfoObj processInfoObj = new ProcessInfoObj();

                processInfoObj.PID = localHostProcess.Id;
                processInfoObj.Name = localHostProcess.ProcessName;

                if (!string.IsNullOrEmpty(localHostProcess.MainWindowTitle))
                {
                    processInfoObj.MainWindowsTitle = localHostProcess.MainWindowTitle;
                }

                processInfoObj.BasePriority = localHostProcess.BasePriority;

                try
                {
                    if (localHostProcess.MainModule != null)
                    {
                        processInfoObj.ExecutablePath = localHostProcess.MainModule.FileName;
                        processInfoObj.InternalName = localHostProcess.MainModule.FileVersionInfo.InternalName;
                        processInfoObj.FileName = localHostProcess.MainModule.FileVersionInfo.FileName;
                        processInfoObj.CompanyName = localHostProcess.MainModule.FileVersionInfo.CompanyName;
                        processInfoObj.EntryPointAddress = localHostProcess.MainModule.EntryPointAddress;
                        processInfoObj.BaseAddress = localHostProcess.MainModule.BaseAddress;
                        processInfoObj.ModuleMemorySize = localHostProcess.MainModule.ModuleMemorySize;
                    }
                }
                catch (Exception ex)
                {
                    // Unable to enumerate the process modules
                    Console.WriteLine(ex.ToString());
                }

                processesInfoByPid.Add(processInfoObj.PID, processInfoObj);
            }

            return processesInfoByPid;
        }

        #endregion
    }
}
