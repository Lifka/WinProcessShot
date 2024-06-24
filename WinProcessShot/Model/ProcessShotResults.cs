using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WinProcessShot.Model
{
    public class ProcessShotResults
    {
        #region MEMBERS
        [Newtonsoft.Json.JsonIgnore]
        public bool ResultReady = false;

        public MODE Mode;

        public DateTime? FirstShotTime = null;
        public DateTime? LastShotTime = null;

        public int NewProcessesTotal { get => NewProcesses.Count<ProcessInfoObj>(); }
        public int FinishedProcessesTotal { get => FinishedProcesses.Count<ProcessInfoObj>(); }
        public int ChangedProcessTotal { get => ChangedProcesses.Count<ChangedProcess>(); }
        public IEnumerable<string> NewProcessesSummary
        {
            get => NewProcesses.Select(item => item.Name).ToList();
        }
        public IEnumerable<string> FinishedProcessesSummary
        {
            get => FinishedProcesses.Select(item => item.Name).ToList();
        }
        public IEnumerable<string> ChangedProcessesSummary
        {
            get => ChangedProcesses.Select(item => item.After.Name).ToList();
        }
        public IEnumerable<ProcessInfoObj> NewProcesses { get; set; } = new List<ProcessInfoObj>();
        public IEnumerable<ProcessInfoObj> FinishedProcesses { get; set; } = new List<ProcessInfoObj>();
        public IEnumerable<ChangedProcess> ChangedProcesses { get; set; } = new List<ChangedProcess>();

        [Newtonsoft.Json.JsonIgnore]
        public HashSet<string> NewProcessesKeys { get; set; } = new HashSet<string>();
        [Newtonsoft.Json.JsonIgnore]
        public HashSet<string> FinishedProcessesKeys { get; set; } = new HashSet<string>();
        [Newtonsoft.Json.JsonIgnore]
        public IDictionary<string, ProcessInfoObj> KnownProcesses = new Dictionary<string, ProcessInfoObj>();


        private static object __lockAddNewProcess = new object();
        private static object __lockAddFinishedProcess = new object();
        private static object __lockAddChangedProcess = new object();
        private bool enabled = true;
        #endregion

        #region METHODS

        public string GetJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public void AddFinishedProcess(ProcessInfoObj processInfoObj)
        {
            if (enabled == false) return;

            lock (__lockAddFinishedProcess)
            {
                if (FinishedProcesses == null)
                {
                    FinishedProcesses = new List<ProcessInfoObj> { processInfoObj };
                }
                else
                {
                    ((List<ProcessInfoObj>)FinishedProcesses).Add(processInfoObj);
                }

                if (NewProcessesKeys == null)
                {
                    FinishedProcessesKeys = new HashSet<string> { processInfoObj.GetProcessKey() };
                }
                else
                {
                    FinishedProcessesKeys.Add(processInfoObj.GetProcessKey());
                }
            }
        }

        public void AddNewProcess(ProcessInfoObj processInfoObj)
        {
            if (enabled == false) return;

            lock (__lockAddNewProcess)
            {

                if (NewProcesses == null)
                {
                    NewProcesses = new List<ProcessInfoObj> { processInfoObj };
                }
                else
                {
                    ((List<ProcessInfoObj>)NewProcesses).Add(processInfoObj);
                }

                string key = processInfoObj.GetProcessKey();
                if (NewProcessesKeys == null)
                {
                    NewProcessesKeys = new HashSet<string> { key };
                }
                else
                {
                    NewProcessesKeys.Add(key);
                }

                KnownProcesses[key] = processInfoObj;
            }
        }

        public void AddShotResultsToResults(ProcessShotResults shotResults)
        {
            if (enabled == false) return;

            if (FirstShotTime == null)
            {
                FirstShotTime = shotResults.FirstShotTime;
            }
            LastShotTime = shotResults.LastShotTime;

            lock (__lockAddNewProcess)
            {
                if (NewProcesses == null)
                {
                    NewProcesses = shotResults.NewProcesses;
                }
                else
                {
                    ((List<ProcessInfoObj>)NewProcesses).AddRange(shotResults.NewProcesses);
                }
            }

            lock (__lockAddFinishedProcess)
            {
                if (FinishedProcesses == null)
                {
                    FinishedProcesses = shotResults.FinishedProcesses;
                }
                else
                {
                    ((List<ProcessInfoObj>)FinishedProcesses).AddRange(shotResults.FinishedProcesses);
                }
            }

            lock (__lockAddChangedProcess)
            {
                if (ChangedProcesses == null)
                {
                    ChangedProcesses = shotResults.ChangedProcesses;
                }
                else
                {
                    ((List<ChangedProcess>)ChangedProcesses).AddRange(shotResults.ChangedProcesses);
                }

                if (NewProcessesKeys == null)
                {
                    NewProcessesKeys = shotResults.NewProcessesKeys;
                }
                else
                {
                    NewProcessesKeys.UnionWith(shotResults.NewProcessesKeys);
                }

                if (FinishedProcessesKeys == null)
                {
                    FinishedProcessesKeys = shotResults.FinishedProcessesKeys;
                }
                else
                {
                    FinishedProcessesKeys.UnionWith(shotResults.FinishedProcessesKeys);
                }
            }
        }

        public void RemoveResultsByPID(int PID)
        {
            if (enabled == false) return;

            lock (__lockAddNewProcess)
            {
                if (NewProcesses != null)
                {
                    ((List<ProcessInfoObj>)NewProcesses).RemoveAll(p => p.PID == PID);
                }
            }

            lock (__lockAddFinishedProcess)
            {
                if (FinishedProcesses != null)
                {
                    ((List<ProcessInfoObj>)FinishedProcesses).RemoveAll(p => p.PID == PID);
                }
            }

            lock (__lockAddChangedProcess)
            {
                if (ChangedProcesses != null)
                {
                    ((List<ChangedProcess>)ChangedProcesses).RemoveAll(p => p.Before.PID == PID);
                }
            }
        }

        public void RemoveResultsByExecutablePath(string executablePath)
        {
            if (enabled == false) return;

            lock (__lockAddNewProcess)
            {
                if (NewProcesses != null)
                {
                    ((List<ProcessInfoObj>)NewProcesses).RemoveAll(p => p.ExecutablePath == executablePath);
                }
            }

            lock (__lockAddFinishedProcess)
            {
                if (FinishedProcesses != null)
                {
                    ((List<ProcessInfoObj>)FinishedProcesses).RemoveAll(p => p.ExecutablePath == executablePath);
                }
            }

            lock (__lockAddChangedProcess)
            {
                if (ChangedProcesses != null)
                {
                    ((List<ChangedProcess>)ChangedProcesses).RemoveAll(p => p.Before.ExecutablePath == executablePath);
                }
            }
        }

        public void RemoveResultsByInternalName(string internalName)
        {
            if (enabled == false) return;

            lock (__lockAddNewProcess)
            {
                if (NewProcesses != null)
                {
                    ((List<ProcessInfoObj>)NewProcesses).RemoveAll(p => p.InternalName == internalName);
                }
            }

            lock (__lockAddFinishedProcess)
            {
                if (FinishedProcesses != null)
                {
                    ((List<ProcessInfoObj>)FinishedProcesses).RemoveAll(p => p.InternalName == internalName);
                }
            }

            lock (__lockAddChangedProcess)
            {
                if (ChangedProcesses != null)
                {
                    ((List<ChangedProcess>)ChangedProcesses).RemoveAll(p => p.Before.InternalName == internalName);
                }
            }
        }

        public void RemoveResultsByName(string name)
        {
            if (enabled == false) return;

            lock (__lockAddNewProcess)
            {
                if (NewProcesses != null)
                {
                    ((List<ProcessInfoObj>)NewProcesses).RemoveAll(p => p.Name == name);
                }
            }

            lock (__lockAddFinishedProcess)
            {
                if (FinishedProcesses != null)
                {
                    ((List<ProcessInfoObj>)FinishedProcesses).RemoveAll(p => p.Name == name);
                }
            }

            lock (__lockAddChangedProcess)
            {
                if (ChangedProcesses != null)
                {
                    ((List<ChangedProcess>)ChangedProcesses).RemoveAll(p => p.Before.Name == name);
                }
            }
        }

        public void Dispose()
        {
            lock (__lockAddNewProcess)
            {
                lock (__lockAddFinishedProcess)
                {
                    lock (__lockAddChangedProcess)
                    {
                        enabled = false;
                        return;
                    }
                }
            }
        }

        #endregion
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum MODE
    {
        BURST_MODE,
        MONITOR_MODE,
        SHOT
    }
}
