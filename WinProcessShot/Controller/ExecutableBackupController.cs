using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WinProcessShot.Controller
{
    public class ExecutableBackupController
    {
        #region MEMBERS

        public static string AppDataDirectory = GetAppDataPathFromProperties();

        #endregion

        #region METHODS

        public static void CreateExecutableBackupForProcess(string executablePath, string processKey)
        {
            if (executablePath == null)
            {
                return;
            }

            string backupDirectory = GetProcessTempDirectory(processKey);
            Directory.CreateDirectory(backupDirectory);
            File.Copy(executablePath, Path.Combine(backupDirectory, Path.GetFileName(executablePath)), true);
        }
        
        public static void CreateExecutableBackupForProcessAsync(string executablePath, string processKey)
        {
            new Task(() => CreateExecutableBackupForProcess(executablePath, processKey)).Start();
        }
        public static bool CopyExecutableBackup(string processKey, string outputDirectory)
        {
            bool result = false;
            string backupDirectory = GetProcessTempDirectory(processKey);
            if (!Directory.Exists(backupDirectory))
            {
                return result;
            }

            string backupFile = null;
            string[] files = Directory.GetFiles(backupDirectory);
            if (files.Any())
            {
                backupFile = files[0];
            }

            if (string.IsNullOrEmpty(backupFile))
            {
                return result;
            }

            try
            {
                File.Copy(backupFile, outputDirectory, true);
                result = true;
            }
            catch
            { }

            return result;
        }

        public static string? GetExecutableBackupName(string processKey)
        {
            string result = null;
            string backupDirectory = GetProcessTempDirectory(processKey);
            if (!Directory.Exists(backupDirectory))
            {
                return result;
            }

            string[] files = Directory.GetFiles(backupDirectory);
            if (files.Any())
            {
                result = Path.GetFileName(files[0]);
            }
            return result;
        }

        public static void ClearBackups()
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
            return Path.Combine(AppDataDirectory, "WinProcessShot", "Backups");
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
