using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinProcessShot.Model;

namespace WinProcessShot.Controller
{
    public class DumpGenerator
    {
        #region MEMBERS

        private static readonly string DUMP_COMMAND = "{0} -accepteula -o -ma \"{1}\"";

        public static string AppDataDirectory = GetAppDataPathFromProperties();

        #endregion

        #region METHODS

        public static void WriteDumpForProcess(int pid, string processKey)
        {
            string dumpDirectory = GetProcessTempDirectory(processKey);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "Resources/Tools/procdump64.exe";
            startInfo.Arguments = string.Format(DUMP_COMMAND, pid, dumpDirectory);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            process.StartInfo = startInfo;

            Directory.CreateDirectory(dumpDirectory);

            process.Start();
            process.WaitForExit();
        }

        public static void WriteDumpForProcessAsync(int pid, string processKey)
        {
            new Task(() => WriteDumpForProcess(pid, processKey)).Start();
        }

        public static bool CopyDump(string processKey, string outputDirectory)
        { 
            bool result = false;
            string dumpDirectory = GetProcessTempDirectory(processKey);
            if (!Directory.Exists(dumpDirectory))
            {
                return result;
            }

            string tempDumpPath = null;
            string[] files = Directory.GetFiles(dumpDirectory);
            if (files.Any())
            {
                tempDumpPath = files[0];
            }

            if (string.IsNullOrEmpty(tempDumpPath))
            {
                return result;
            }

            try
            {
                File.Copy(tempDumpPath, outputDirectory, true);
                result = true;
            }
            catch
            { }

            return result;
        }

        public static bool DeleteDump(string processKey)
        {
            bool result = false;
            string dumpDirectory = GetProcessTempDirectory(processKey);
            if (!Directory.Exists(dumpDirectory))
            {
                return result;
            }

            try
            {
                Directory.Delete(dumpDirectory, true);
                result = true;
            }
            catch
            { }

            return result;
        }

        public static string? GetDumpName(string processKey)
        {
            string result = null;
            string dumpDirectory = GetProcessTempDirectory(processKey);
            if (!Directory.Exists(dumpDirectory))
            {
                return result;
            }

            string[] files = Directory.GetFiles(dumpDirectory);
            if (files.Any())
            {
                result = Path.GetFileName(files[0]);
            }
            return result;
        }

        public static void ClearDumps()
        {
            if (Directory.Exists(GetProcessTempBaseDirectory()))
            {
                Directory.Delete(GetProcessTempBaseDirectory(), true);
            }
        }

        private static string GetProcessTempDirectory(string processKey)
        {
            string basePath = GetProcessTempBaseDirectory();
            if (!Directory.Exists(basePath))
            { 
                Directory.CreateDirectory(basePath);
            }

            return Path.Combine(basePath, processKey.ToString());
        }

        private static string GetProcessTempBaseDirectory()
        {
            return Path.Combine(AppDataDirectory, "WinProcessShot", "Dumps");
        }

        private static string GetAppDataPathFromProperties()
        {
            string path = System.IO.Path.GetTempPath();
            if (Properties.Settings.Default == null)
            {
                return path;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.AppDataDirectory))
            {
                path = Properties.Settings.Default.AppDataDirectory;
            }

            return path;
        }

        #endregion
    }
}
