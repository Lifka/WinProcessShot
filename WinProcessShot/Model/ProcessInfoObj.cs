using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Cryptography;

namespace WinProcessShot.Model
{
    public class ProcessInfoObj
    {
        #region MEMBERS
        public int PID { get; set; }
        public string? Name { get; set; }
        public uint? PPID { get; set; }
        public string? ParentProcessName { get; set; }
        public string? MainWindowsTitle { get; set; }
        public string? ExecutablePath { get; set; }
        public string? CompanyName { get;  set; }
        public string? InternalName { get; set; }
        public string? FileName { get;  set; }
        public string? Command { get; set; }
        public nint? BaseAddress { get; set; }
        public nint? EntryPointAddress { get; set; }
        public int? ModuleMemorySize { get; set; }
        public int BasePriority { get; set; }
        public string? MD5 { get; set; }
        public bool? Trusted { get; set; } = null;
        public uint ExitCode { get; set; }

        public DateTime SeenAt { get; set; } = DateTime.Now;

        #endregion

        #region METHODS
        public string GetProcessKey()
        {
            return Path.GetFileName(ExecutablePath) + PID;
        }

        public void CalculateMD5()
        {
            if (string.IsNullOrEmpty(ExecutablePath)
                || !File.Exists(ExecutablePath))
            {
                return;
            }

            try
            {
                using (var createdMD5 = System.Security.Cryptography.MD5.Create())
                {
                    using (var stream = File.OpenRead(ExecutablePath))
                    {
                        var hash = createdMD5.ComputeHash(stream);
                        this.MD5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch { }
        }

        public void LoadCommandLine()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(string.Format("SELECT CommandLine FROM Win32_Process WHERE ProcessId = {0}", PID)))
            using (ManagementObjectCollection objects = searcher.Get())
            {
                Command = objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
            }
        }

        public void LoadParentProcess()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", PID)))
            using (ManagementObjectCollection objects = searcher.Get())
            {
                var ppidObj = objects.Cast<ManagementBaseObject>().SingleOrDefault()?["ParentProcessId"];
                if (ppidObj != null)
                {
                    PPID = (uint)ppidObj;
                    try
                    {
                        ParentProcessName = Process.GetProcessById((int)PPID).ProcessName;
                    }
                    catch { }
                }
            }
        }
        #endregion
    }
}
