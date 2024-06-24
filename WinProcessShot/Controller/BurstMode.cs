using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinProcessShot.Model;

namespace WinProcessShot.Controller
{
    internal static class BurstMode
    {
        #region MEMBERS

        private static Shot? lastShot = null;
        private static Queue<Shot> shotsQueue = new Queue<Shot>();
        private static List<Shot> previousShots = new List<Shot>();
        private static bool burstModeActive = false;

        static object __lockPreviousShotsListsObj = new object();
        static object __lockShotsQueueObj = new object();

        public static ProcessShotResults? Results { get; set; } = null;
        public static readonly int N_CALC_RESULTS_TAKS = 2;
        public static bool EnableAutomaticDump { get; set; } = false;
        public static bool EnableAutomaticExeBackup { get; set; } = false;

        #endregion

        #region METHODS

        public static void StartBurstMode(int timeBetweenShotsMS)
        {
            LoadOptions();
            Results = new ProcessShotResults();

            Results.Mode = MODE.BURST_MODE;
            burstModeActive = true;

            new Task(() => BurstModeTask(timeBetweenShotsMS)).Start();
            new Task(() => ManageCalculateResults(timeBetweenShotsMS)).Start();
        }

        public static void StopBurstMode()
        {
            burstModeActive = false;
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

        private static void BurstModeTask(int timeBetweenShotsMS)
        {
            while (burstModeActive)
            {
                TakeShot();
                Thread.Sleep(timeBetweenShotsMS);
            }
        }

        private static void ManageCalculateResults(int timeBetweenShotsMS)
        {
            for (int i = 0; i < N_CALC_RESULTS_TAKS; i++)
            {
                new Task(() => CalculateResultsTask()).Start();

                Thread.Sleep(timeBetweenShotsMS / N_CALC_RESULTS_TAKS);
            }
            
        }

        private static void CalculateResultsTask()
        {
            //DateTime? lastShotChecked = null;
            while (burstModeActive || shotsQueue.Count() > 0) 
            {
                Shot currentShot;
                Shot previousShot;
                lock (__lockShotsQueueObj)
                {
                    if (shotsQueue.Count() == 0)
                    {
                        continue;
                    }
                    currentShot = shotsQueue.Dequeue();

                    /*
                    if (lastShotChecked.HasValue
                        && lastShotChecked.Value > currentShot.TimeStamp)
                    {
                        continue;
                    }
                    lastShotChecked = currentShot.TimeStamp;
                    */

                    if (lastShot == null)
                    {
                        lastShot = currentShot;
                        continue;
                    }

                    previousShot = lastShot;
                    lastShot = currentShot;
                }

                List<Shot> copyPreviousShots;
                lock (__lockPreviousShotsListsObj)
                {
                    copyPreviousShots = new List<Shot>(previousShots);
                }

                ProcessShotResults results = ProcessDifferencesShot.GenerateResults(previousShot, currentShot, copyPreviousShots, EnableAutomaticDump, EnableAutomaticExeBackup);

                lock (__lockPreviousShotsListsObj)
                {
                    previousShots.Add(previousShot);
                }

                AddShotResultsToResults(results);
            }
        }

        private static void TakeShot()
        {
            Task takeShotTask = new Task(() => TakeShotTask());
            takeShotTask.Start();
        }

        private static void TakeShotTask()
        {
            Shot shot = new Shot();

            lock (__lockShotsQueueObj)
            {
                shotsQueue.Enqueue(shot);
            }
        }

        internal static void Clear()
        {
            lastShot = null;
            if (Results != null)
            { 
                Results.Dispose();
            }
            Results = null;
            burstModeActive = false;
        }

        private static void AddShotResultsToResults(ProcessShotResults shotResults)
        {
            if (Results == null)
            {
                return;
            }

            Results.AddShotResultsToResults(shotResults);
            Results.ResultReady = true;
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
