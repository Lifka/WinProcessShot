using System.Diagnostics;
using System.Management;
using System.Security.Cryptography;
using WinProcessShot.Controller;
using WinProcessShot.Model;
using WinProcessShot.View;

namespace WinProcessShot
{
    public partial class Main : Form
    {
        #region MEMBERS

        private bool firstShotTaken = false;
        private bool burstModeActive = false;
        private bool monitorModeActive = false;
        private Stopwatch elapsedTime;
        private bool diabledPreviewTable = false;
        private HashSet<string> newProcessAlreadyAdded = new HashSet<string>();
        private HashSet<string> finishedProcessAlreadyAdded = new HashSet<string>();
        private HashSet<string> changedProcessAlreadyAdded = new HashSet<string>();
        private DataGridView currentVisibleGridView = null;

        // lockers
        static object __lockUpdateNewProcessGridViewObj = new object();
        static object __lockUpdateFinishedProcessGridViewObj = new object();
        static object __lockUpdateChangedProcessGridViewObj = new object();

        #endregion

        #region CONSTRUCTORS

        public Main()
        {
            InitializeComponent();
        }

        #endregion


        private void Form1_Load(object sender, EventArgs e)
        {
            saveAsJSONToolStripMenuItem.Enabled = false;
            FiltersController.LoadFilters();
            UpdateFiltersButtonText();
            TabPanel_SelectedIndexChanged(null, null);

        }

        #region MENU

        #region SAVE_JSON

        private void SaveJSON()
        {
            if ((ProcessDifferencesShot.Results == null || ProcessDifferencesShot.Results.ResultReady == false)
                && (BurstMode.Results == null || BurstMode.Results.ResultReady == false))
            {
                return;
            }

            string? json = null;
            try
            {
                if (ProcessDifferencesShot.Results != null)
                {
                    json = ProcessDifferencesShot.Results.GetJSON();
                }
                else if (BurstMode.Results != null)
                {
                    json = BurstMode.Results.GetJSON();
                }
            }
            catch (Exception)
            { }

            if (json != null)
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = "WinProcessShot";
                savefile.Filter = "JSON file (*.json)|*.json|All files (*.*)|*.*";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter streamWriter = new StreamWriter(savefile.FileName))
                    {
                        streamWriter.Write(json);
                    }
                }
            }
        }

        private void saveAsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveJSON();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S && saveAsJSONToolStripMenuItem.Enabled)
            {
                SaveJSON();
            }
        }

        #endregion

        private void ExitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ClearAction();
            Environment.Exit(Environment.ExitCode);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            aboutForm.ShowDialog();
        }

        private void FiltersButton_Click(object sender, EventArgs e)
        {
            Filters filtersForm = new Filters(this);
            filtersForm.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options optionsForm = new Options();
            optionsForm.ShowDialog();
        }

        #endregion

        #region HANDLE_BUTTONS

        private void FirstShotButton_Click(object sender, EventArgs e)
        {
            if (!firstShotTaken)
            {
                ClearAction();
                ShotButton.Enabled = false;
                BurstModeButton.Enabled = false;
                FiltersButton.Enabled = false;
                optionsToolStripMenuItem.Enabled = false;
                BurstModeMS.Enabled = false;
                MonitorButton.Enabled = false;

                ProcessDifferencesShot.TakeFirstShot();

                ShotButton.Text = "2nd shot";

                RunElapsedTime();

                firstShotTaken = true;
                ClearButton.Enabled = true;
                ShotButton.Enabled = true;

                SetDefaultAppIcon();
            }
            else
            {
                ProcessDifferencesShot.TakeLastShot();

                ProcessDifferencesShot.GenerateShotResults();
                StopElapsedTime();
                SetResultsShot();
                saveAsJSONToolStripMenuItem.Enabled = true;
                FiltersButton.Enabled = true;
                optionsToolStripMenuItem.Enabled = true;
                BurstModeMS.Enabled = true;

                ResetShotButton();
                ResetBurstModeButton();
                ResetMonitorButton();

                LoadShotResultsInGridView();

                SetDefaultAppIcon();
                firstShotTaken = false;
            }
        }

        private void ResetShotButton()
        {
            ShotButton.Text = "1st shot";
            ShotButton.Enabled = true;
        }

        private void ResetMonitorButton()
        {
            MonitorButton.Text = "Monitor";
            MonitorButton.Enabled = true;
        }

        private void ResetBurstModeButton()
        {
            BurstModeButton.Text = "Burst mode";
            BurstModeButton.Enabled = true;
        }

        private void ClearShotButton_Click(object sender, EventArgs e)
        {
            ClearAction();
        }

        private void MonitorModeButton_Click(object sender, EventArgs e)
        {
            if (!monitorModeActive)
            {
                SetMonitoringAppIcon();
                SetMonitorModeActiveIcon();

                ClearAction();
                monitorModeActive = true;
                ShotButton.Enabled = false;
                BurstModeButton.Enabled = false;
                FiltersButton.Enabled = false;
                optionsToolStripMenuItem.Enabled = false;
                BurstModeMS.Enabled = false;
                MonitorButton.Text = "Stop monitoring";

                RunElapsedTime();
                ETWMonitorMode.StartMonitorMode();

                Task updateMonitorModeResults = new Task(() => UpdateMonitorModeResults());
                updateMonitorModeResults.Start();
            }
            else
            {
                ETWMonitorMode.StopMonitorMode();
                StopElapsedTime();

                ResetShotButton();
                ResetBurstModeButton();
                ResetMonitorButton();

                monitorModeActive = false;
                saveAsJSONToolStripMenuItem.Enabled = true;
                FiltersButton.Enabled = true;
                optionsToolStripMenuItem.Enabled = true;
                BurstModeMS.Enabled = true;
                ClearButton.Enabled = true;

                SetDefaultAppIcon();
                SetMonitorModeDefaultIcon();
            }

        }

        private void BurstModeButton_Click(object sender, EventArgs e)
        {
            if (!burstModeActive)
            {
                SetMonitoringAppIcon();
                SetBurstModeActiveIcon();

                ClearAction();
                burstModeActive = true;
                ShotButton.Enabled = false;
                MonitorButton.Enabled = false;
                FiltersButton.Enabled = false;
                optionsToolStripMenuItem.Enabled = false;
                BurstModeButton.Text = "Stop burst mode";
                BurstModeMS.Enabled = false;

                int timeBetweenShots = int.Parse(BurstModeMS.Text);
                if (timeBetweenShots < 50)
                {
                    timeBetweenShots = 1500;
                    BurstModeMS.Text = "1500";
                }

                BurstMode.StartBurstMode(timeBetweenShots);
                RunElapsedTime();

                Task updateBurstModeResults = new Task(() => UpdateBurstModeResults());
                updateBurstModeResults.Start();
            }
            else
            {
                BurstMode.StopBurstMode();
                StopElapsedTime();

                ResetShotButton();
                ResetBurstModeButton();
                ResetMonitorButton();

                burstModeActive = false;
                saveAsJSONToolStripMenuItem.Enabled = true;
                optionsToolStripMenuItem.Enabled = true;
                FiltersButton.Enabled = true;
                ClearButton.Enabled = true;
                BurstModeMS.Enabled = true;

                SetDefaultAppIcon();
                SetBurstModeDefaultIcon();
            }

        }
        #endregion

        #region RESULTS

        public void UpdateFiltersButtonText()
        {
            FiltersButton.Text = string.Format("Filters ({0})", FiltersController.CurrentFilters.Count());
        }

        private void SetResultsShot()
        {
            if (ProcessDifferencesShot.Results == null)
            {
                return;
            }

            CreatedProcessLabel.Text = ProcessDifferencesShot.Results.NewProcessesTotal.ToString();
            FinishedProcessLabel.Text = ProcessDifferencesShot.Results.FinishedProcessesTotal.ToString();
            ProcessChangedLabel.Text = ProcessDifferencesShot.Results.ChangedProcessTotal.ToString();
        }


        private void RunElapsedTime()
        {
            elapsedTime = new Stopwatch();
            elapsedTime.Start();
            ElapsedTimeTimer.Enabled = true;
        }

        private void StopElapsedTime()
        {
            ElapsedTimeTimer.Enabled = false;
            if (elapsedTime != null)
            {
                elapsedTime.Stop();
                elapsedTime = null;
            }
        }

        private void UpdateMonitorModeResults()
        {
            while (monitorModeActive)
            {
                Thread.Sleep(100);
                SetResultsMonitorMode();
            }
        }

        private void SetResultsMonitorMode()
        {
            if (ETWMonitorMode.Results == null)
            {
                return;
            }

            SetTextNewProcessTotal(ETWMonitorMode.Results.NewProcessesTotal.ToString());
            SetTextFinishedProcessTotal(ETWMonitorMode.Results.FinishedProcessesTotal.ToString());
            SetTextProcessChangedTotal(ETWMonitorMode.Results.ChangedProcessTotal.ToString());

            if (!diabledPreviewTable && ETWMonitorMode.Results != null)
            {
                if (ETWMonitorMode.Results.NewProcesses != null)
                {
                    SetNewProcess(ETWMonitorMode.Results.NewProcesses);
                }

                if (ETWMonitorMode.Results.FinishedProcesses != null)
                {
                    SetFinishedProcess(ETWMonitorMode.Results.FinishedProcesses);
                }

                if (ETWMonitorMode.Results.ChangedProcesses != null)
                {
                    SetChangedProcess(ETWMonitorMode.Results.ChangedProcesses);
                }
            }
        }

        private void UpdateBurstModeResults()
        {
            while (burstModeActive)
            {
                Thread.Sleep(100);
                SetResultsBurstMode();
            }
        }

        private void SetResultsBurstMode()
        {
            if (BurstMode.Results == null)
            {
                return;
            }

            SetTextNewProcessTotal(BurstMode.Results.NewProcessesTotal.ToString());
            SetTextFinishedProcessTotal(BurstMode.Results.FinishedProcessesTotal.ToString());
            SetTextProcessChangedTotal(BurstMode.Results.ChangedProcessTotal.ToString());

            if (!diabledPreviewTable && BurstMode.Results != null)
            {
                if (BurstMode.Results.NewProcesses != null)
                {
                    SetNewProcess(BurstMode.Results.NewProcesses);
                }

                if (BurstMode.Results.FinishedProcesses != null)
                {
                    SetFinishedProcess(BurstMode.Results.FinishedProcesses);
                }

                if (BurstMode.Results.ChangedProcesses != null)
                {
                    SetChangedProcess(BurstMode.Results.ChangedProcesses);
                }
            }
        }

        delegate void SetProcessInfoObjArrayCallback(IEnumerable<ProcessInfoObj> processInfoObjs);

        private void SetNewProcess(IEnumerable<ProcessInfoObj> newProcesses)
        {
            if (DataGridViewNewProcess.InvokeRequired)
            {
                SetProcessInfoObjArrayCallback d = new SetProcessInfoObjArrayCallback(SetNewProcess);
                Invoke(d, new object[] { newProcesses });
            }
            else
            {
                LoadNewProcessInGridView(newProcesses);
            }
        }

        private void LoadNewProcessInGridView(IEnumerable<ProcessInfoObj> newProcesses)
        {
            lock (__lockUpdateNewProcessGridViewObj)
            {
                IEnumerable<ProcessInfoObj> newProcessesCopy = new List<ProcessInfoObj>(newProcesses);
                foreach (ProcessInfoObj process in newProcessesCopy)
                {
                    string[] values = GetValuesFromProccessInfoObj(process, out string key);
                    if (DataGridViewNewProcess != null && !newProcessAlreadyAdded.Contains(key))
                    {
                        newProcessAlreadyAdded.Add(key);
                        DataGridViewNewProcess.Rows.Add(values);
                    }
                }
            }
        }

        private void ReloadAllNewProcessInGridView()
        {
            lock (__lockUpdateNewProcessGridViewObj)
            {
                IEnumerable<ProcessInfoObj> newProcesses = null;

                if (BurstMode.Results != null)
                {
                    newProcesses = BurstMode.Results.NewProcesses;
                }
                else if (ETWMonitorMode.Results != null)
                {
                    newProcesses = ETWMonitorMode.Results.NewProcesses;
                }

                if (newProcesses == null || DataGridViewNewProcess == null)
                {
                    return;
                }

                DataGridViewNewProcess.Rows.Clear();
                foreach (ProcessInfoObj process in newProcesses)
                {
                    string[] values = GetValuesFromProccessInfoObj(process, out string key);
                    newProcessAlreadyAdded.Add(key);
                    DataGridViewNewProcess.Rows.Add(values);
                }
            }
        }

        private void LoadShotResultsInGridView()
        {
            if (ProcessDifferencesShot.Results == null)
            {
                return;
            }

            if (ProcessDifferencesShot.Results.NewProcesses != null)
            {
                LoadNewProcessInGridView(ProcessDifferencesShot.Results.NewProcesses);
            }

            if (ProcessDifferencesShot.Results.FinishedProcesses != null)
            {
                LoadFinishedProcessInGridView(ProcessDifferencesShot.Results.FinishedProcesses);
            }

            if (ProcessDifferencesShot.Results.ChangedProcesses != null)
            {
                LoadChangedProcessInGridView(ProcessDifferencesShot.Results.ChangedProcesses);
            }
        }

        private void SetFinishedProcess(IEnumerable<ProcessInfoObj> finishedProcesses)
        {
            if (this.DataGridViewFinishedProcess.InvokeRequired)
            {
                SetProcessInfoObjArrayCallback d = new SetProcessInfoObjArrayCallback(SetFinishedProcess);
                this.Invoke(d, new object[] { finishedProcesses });
            }
            else
            {
                LoadFinishedProcessInGridView(finishedProcesses);
            }
        }

        private void ReloadAllFinishedProcessInGridView()
        {
            lock (__lockUpdateFinishedProcessGridViewObj)
            {
                IEnumerable<ProcessInfoObj> finishedProcesses = null;

                if (burstModeActive && BurstMode.Results != null)
                {
                    finishedProcesses = BurstMode.Results.FinishedProcesses;
                }
                else if (monitorModeActive && ETWMonitorMode.Results != null)
                {
                    finishedProcesses = ETWMonitorMode.Results.FinishedProcesses;
                }

                if (finishedProcesses == null || DataGridViewNewProcess == null)
                {
                    return;
                }

                if (finishedProcesses == null
                    || DataGridViewFinishedProcess == null)
                {
                    return;
                }

                DataGridViewFinishedProcess.Rows.Clear();
                foreach (ProcessInfoObj process in finishedProcesses)
                {
                    string[] values = GetValuesFromProccessInfoObj(process, out string key);
                    finishedProcessAlreadyAdded.Add(key);
                    DataGridViewFinishedProcess.Rows.Add(values);
                }
            }
        }

        private void LoadFinishedProcessInGridView(IEnumerable<ProcessInfoObj> finishedProcesses)
        {
            lock (__lockUpdateFinishedProcessGridViewObj)
            {
                IEnumerable<ProcessInfoObj> finishedProcessesCopy = new List<ProcessInfoObj>(finishedProcesses);
                foreach (ProcessInfoObj process in finishedProcessesCopy)
                {
                    string[] values = GetValuesFromProccessInfoObj(process, out string key);
                    if (DataGridViewFinishedProcess != null && !finishedProcessAlreadyAdded.Contains(key))
                    {
                        finishedProcessAlreadyAdded.Add(key);
                        DataGridViewFinishedProcess.Rows.Add(values);
                    }
                }
            }
        }

        private string[] GetValuesFromProccessInfoObj(ProcessInfoObj process, out string key)
        {
            string seenAt = process.SeenAt.ToString();
            string name = process.Name != null ? process.Name : string.Empty;
            string pID = process.PID.ToString();
            string intenalName = process.InternalName != null ? process.InternalName : string.Empty;
            string executablePath = process.ExecutablePath != null ? process.ExecutablePath : string.Empty;
            string companyName = process.CompanyName != null ? process.CompanyName : string.Empty;
            string parentName = process.ParentProcessName != null ? process.ParentProcessName : string.Empty;
            string parent = process.PPID != null ? string.Format("{0} ({1})", parentName, process.PPID) : string.Empty;
            string mD5 = process.MD5 != null ? process.MD5 : string.Empty;
            string trusted = string.Empty;
            string command = process.Command != null ? process.Command : string.Empty;
            string basePriority = process.BasePriority.ToString();
            string baseAddress = process.BaseAddress.HasValue ? process.BaseAddress.Value.ToString() : string.Empty;
            string entryPointAddress = process.EntryPointAddress.HasValue ? process.EntryPointAddress.Value.ToString() : string.Empty;
            string moduleMemorySize = process.ModuleMemorySize.HasValue ? process.ModuleMemorySize.Value.ToString() : string.Empty;

            bool isTrusted = process.Trusted.HasValue ? process.Trusted.Value : false;
            if (isTrusted)
            {
                trusted = "✓";
            }

            key = string.Format("{0}{1}{2}", seenAt, name, pID);

            return new string[] {
                seenAt,
                name,
                pID,
                intenalName,
                executablePath,
                companyName,
                parent,
                mD5,
                trusted,
                command,
                basePriority,
                baseAddress,
                entryPointAddress,
                moduleMemorySize
            };
        }

        delegate void SetChangedProcessCallback(IEnumerable<ChangedProcess> changedProcesses);

        private void SetChangedProcess(IEnumerable<ChangedProcess> changedProcesses)
        {
            if (this.DataGridViewFinishedProcess.InvokeRequired)
            {
                SetChangedProcessCallback d = new SetChangedProcessCallback(SetChangedProcess);
                this.Invoke(d, new object[] { changedProcesses });
            }
            else
            {
                LoadChangedProcessInGridView(changedProcesses);
            }
        }

        private void LoadChangedProcessInGridView(IEnumerable<ChangedProcess> changedProcesses)
        {
            foreach (ChangedProcess process in changedProcesses)
            {
                string[] values = GetValuesFromChangedProcess(process, out string key);
                if (DataGridViewFinishedProcess != null && !changedProcessAlreadyAdded.Contains(key))
                {
                    changedProcessAlreadyAdded.Add(key);
                    DataGridViewProcessChanged.Rows.Add(values);
                }
            }
        }

        private void ReloadAllChangedProcessInGridView()
        {
            lock (__lockUpdateChangedProcessGridViewObj)
            {
                IEnumerable<ChangedProcess> changedProcesses = null;

                if (burstModeActive && BurstMode.Results != null)
                {
                    changedProcesses = BurstMode.Results.ChangedProcesses;
                }
                else if (monitorModeActive && ETWMonitorMode.Results != null)
                {
                    changedProcesses = ETWMonitorMode.Results.ChangedProcesses;
                }

                if (changedProcesses == null || DataGridViewNewProcess == null)
                {
                    return;
                }

                if (changedProcesses == null
                    || DataGridViewFinishedProcess == null)
                {
                    return;
                }

                DataGridViewProcessChanged.Rows.Clear();
                foreach (ChangedProcess process in changedProcesses)
                {
                    string[] values = GetValuesFromChangedProcess(process, out string key);
                    changedProcessAlreadyAdded.Add(key);
                    DataGridViewProcessChanged.Rows.Add(values);
                }
            }
        }

        private string[] GetValuesFromChangedProcess(ChangedProcess changedProcess, out string key)
        {
            key = string.Empty;
            if (changedProcess == null || changedProcess.After == null || changedProcess.Before == null)
            {
                return new string[] { };
            }

            string pID = changedProcess.Before.PID.ToString();
            string seenProccessAt = changedProcess.Before.SeenAt.ToString();
            string seenProcessChangedAt = changedProcess.After.SeenAt.ToString();
            string nameBefore = changedProcess.Before.Name != null ? changedProcess.Before.Name : string.Empty;
            string nameAfter = changedProcess.After.Name != null ? changedProcess.After.Name : string.Empty;
            string internalNameBefore = changedProcess.Before.InternalName != null ? changedProcess.Before.InternalName : string.Empty;
            string internalNameAfter = changedProcess.After.InternalName != null ? changedProcess.After.InternalName : string.Empty;
            string executablePathBefore = changedProcess.Before.ExecutablePath != null ? changedProcess.Before.ExecutablePath : string.Empty;
            string executablePathAfter = changedProcess.After.ExecutablePath != null ? changedProcess.After.ExecutablePath : string.Empty;
            string companyNameBefore = changedProcess.Before.CompanyName != null ? changedProcess.Before.CompanyName : string.Empty;
            string companyNameAfter = changedProcess.After.CompanyName != null ? changedProcess.After.CompanyName : string.Empty;
            string basePriorityBefore = changedProcess.Before.BasePriority.ToString();
            string basePriorityAfter = changedProcess.After.BasePriority.ToString();
            string baseAddressBefore = changedProcess.Before.BaseAddress.HasValue ? changedProcess.Before.BaseAddress.Value.ToString() : string.Empty;
            string baseAddressAfter = changedProcess.After.BaseAddress.HasValue ? changedProcess.After.BaseAddress.Value.ToString() : string.Empty;
            string entryPointAddressBefore = changedProcess.Before.EntryPointAddress.HasValue ? changedProcess.Before.EntryPointAddress.Value.ToString() : string.Empty;
            string entryPointAddressAfter = changedProcess.After.EntryPointAddress.HasValue ? changedProcess.After.EntryPointAddress.Value.ToString() : string.Empty;
            string moduleMemorySizeBefore = changedProcess.Before.ModuleMemorySize.HasValue ? changedProcess.Before.ModuleMemorySize.Value.ToString() : string.Empty;
            string moduleMemorySizeAfter = changedProcess.After.ModuleMemorySize.HasValue ? changedProcess.After.ModuleMemorySize.Value.ToString() : string.Empty;

            key = string.Format("{0}{1}{2}", pID, seenProccessAt, seenProcessChangedAt);

            return new string[] {
                pID,
                seenProccessAt,
                seenProcessChangedAt,
                nameBefore,
                nameAfter,
                internalNameBefore,
                internalNameAfter,
                executablePathBefore,
                executablePathAfter,
                companyNameBefore,
                companyNameAfter,
                basePriorityBefore,
                basePriorityAfter,
                baseAddressBefore,
                baseAddressAfter,
                entryPointAddressBefore,
                entryPointAddressAfter,
                moduleMemorySizeBefore,
                moduleMemorySizeAfter
            };
        }

        delegate void SetTextCallback(string text);

        private void SetTextNewProcessTotal(string text)
        {
            if (this.CreatedProcessLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextNewProcessTotal);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.CreatedProcessLabel.Text = text;
            }
        }

        private void SetTextFinishedProcessTotal(string text)
        {
            if (this.FinishedProcessLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextFinishedProcessTotal);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.FinishedProcessLabel.Text = text;
            }
        }

        private void SetTextProcessChangedTotal(string text)
        {
            if (this.ProcessChangedLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextProcessChangedTotal);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.ProcessChangedLabel.Text = text;
            }
        }

        private void ElapsedTimeTimer_Tick(object sender, EventArgs e)
        {
            ElapsedTimeLabel.Text = string.Format("Elapsed time: {0:00}:{1:00}:{2:00}",
                elapsedTime.Elapsed.Hours, elapsedTime.Elapsed.Minutes, elapsedTime.Elapsed.Seconds);
        }


        private void DisablePreviewTableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            diabledPreviewTable = DisablePreviewTableCheckBox.Checked;

            if (diabledPreviewTable)
            {
                DataGridViewNewProcess.Enabled = false;
                DataGridViewFinishedProcess.Enabled = false;
                DataGridViewProcessChanged.Enabled = false;
                ClearGridViews();
            }
            else
            {
                DataGridViewNewProcess.Enabled = true;
                DataGridViewFinishedProcess.Enabled = true;
                DataGridViewProcessChanged.Enabled = true;

                if (!burstModeActive
                    && BurstMode.Results != null)
                {
                    if (BurstMode.Results.NewProcesses != null)
                    {
                        LoadNewProcessInGridView(BurstMode.Results.NewProcesses);
                    }

                    if (BurstMode.Results.FinishedProcesses != null)
                    {
                        LoadFinishedProcessInGridView(BurstMode.Results.FinishedProcesses);
                    }

                    if (BurstMode.Results.ChangedProcesses != null)
                    {
                        LoadChangedProcessInGridView(BurstMode.Results.ChangedProcesses);
                    }
                }
                else
                {
                    LoadShotResultsInGridView();
                }
            }
        }


        private void GridViewSelectRow(DataGridView gridView, int rowSelected)
        {
            if (rowSelected != -1)
            {
                gridView.ClearSelection();
                gridView.Rows[rowSelected].Selected = true;
            }

        }

        private void DataGridViewNewProcesses_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GridViewSelectRow(DataGridViewNewProcess, e.RowIndex);

                try
                {
                    CellContextMenu.Show(Cursor.Position.X, Cursor.Position.Y);

                }
                catch (Exception)
                { }
            }
        }

        private void DataGridViewFinishedProcess_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GridViewSelectRow(DataGridViewFinishedProcess, e.RowIndex);

                try
                {
                    CellContextMenu.Show(Cursor.Position.X, Cursor.Position.Y);

                }
                catch (Exception)
                { }
            }
        }

        private void DataGridViewProcessChanged_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GridViewSelectRow(DataGridViewProcessChanged, e.RowIndex);

                try
                {
                    CellContextMenu.Show(Cursor.Position.X, Cursor.Position.Y);

                }
                catch (Exception)
                { }
            }
        }

        private void ReloadGridViews()
        {
            ReloadAllNewProcessInGridView();
            ReloadAllChangedProcessInGridView();
            ReloadAllFinishedProcessInGridView();
        }

        private void TabPanel_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (TabPanel.SelectedIndex == 0)
            {
                currentVisibleGridView = DataGridViewNewProcess;
            }
            else if (TabPanel.SelectedIndex == 1)
            {
                currentVisibleGridView = DataGridViewFinishedProcess;
            }
            else if (TabPanel.SelectedIndex == 2)
            {
                currentVisibleGridView = DataGridViewProcessChanged;
            }
        }

        private void BurstModeM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        #endregion

        #region CLEAR
        private void ClearAction()
        {
            StopElapsedTime();

            ProcessDifferencesShot.Clear();
            BurstMode.Clear();
            DumpGenerator.ClearDumps();
            ExecutableBackupController.ClearBackups();

            firstShotTaken = false;
            ClearButton.Enabled = false;
            saveAsJSONToolStripMenuItem.Enabled = false;
            FiltersButton.Enabled = true;
            optionsToolStripMenuItem.Enabled = true;
            BurstModeMS.Enabled = true;

            ResetBurstModeButton();
            ResetShotButton();
            ResetMonitorButton();

            CreatedProcessLabel.Text = "0";
            FinishedProcessLabel.Text = "0";
            ProcessChangedLabel.Text = "0";
            ElapsedTimeLabel.Text = "Elapsed time: 00:00:00";
            ClearGridViews();
        }

        private void ClearDataGridViewProcessCreated()
        {
            DataGridViewNewProcess.Rows.Clear();
            newProcessAlreadyAdded = new HashSet<string>();
        }

        private void ClearDataGridViewProcessDeleted()
        {
            DataGridViewFinishedProcess.Rows.Clear();
            finishedProcessAlreadyAdded = new HashSet<string>();
        }

        private void ClearDataGridViewProcessChanged()
        {
            DataGridViewProcessChanged.Rows.Clear();
            changedProcessAlreadyAdded = new HashSet<string>();
        }

        private void ClearGridViews()
        {
            ClearDataGridViewProcessCreated();
            ClearDataGridViewProcessDeleted();
            ClearDataGridViewProcessChanged();
        }
        #endregion

        #region ICONS

        private void SetDefaultAppIcon()
        {
            Type type = this.GetType();
            System.Resources.ResourceManager resources =
            new System.Resources.ResourceManager(type.Namespace + ".Properties.Resources", this.GetType().Assembly);

            this.Icon = (System.Drawing.Icon)resources.GetObject("logo_wps100_w");
        }

        private void SetMonitoringAppIcon()
        {
            Type type = this.GetType();
            System.Resources.ResourceManager resources =
                new System.Resources.ResourceManager(type.Namespace + ".Properties.Resources", this.GetType().Assembly);

            this.Icon = (System.Drawing.Icon)resources.GetObject("logo_wps_100_monitoring");
        }

        private void SetBurstModeDefaultIcon()
        {
            Type type = this.GetType();
            System.Resources.ResourceManager resources =
            new System.Resources.ResourceManager(type.Namespace + ".Properties.Resources", this.GetType().Assembly);

            BurstModeButton.Image = (System.Drawing.Image)resources.GetObject("burst_mode_icon");
        }

        private void SetBurstModeActiveIcon()
        {
            Type type = this.GetType();
            System.Resources.ResourceManager resources =
                new System.Resources.ResourceManager(type.Namespace + ".Properties.Resources", this.GetType().Assembly);

            BurstModeButton.Image = (System.Drawing.Image)resources.GetObject("burst_mode_icon_active");
        }

        private void SetMonitorModeActiveIcon()
        {
            Type type = this.GetType();
            System.Resources.ResourceManager resources =
                new System.Resources.ResourceManager(type.Namespace + ".Properties.Resources", this.GetType().Assembly);

            MonitorButton.Image = (System.Drawing.Image)resources.GetObject("monitoring_icon");
        }

        private void SetMonitorModeDefaultIcon()
        {
            Type type = this.GetType();
            System.Resources.ResourceManager resources =
            new System.Resources.ResourceManager(type.Namespace + ".Properties.Resources", this.GetType().Assembly);

            MonitorButton.Image = (System.Drawing.Image)resources.GetObject("monitor_icon");
        }

        #endregion

        #region TABLE_CONTEXT_MENU

        private void ToolStripMenuItem1_FilterName(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            FilterSelectedProperty(FilterPropertiesEnum.Name, out string? propertyColumnValue);
            if (!string.IsNullOrEmpty(propertyColumnValue))
            {
                BurstMode.RemoveResultsByName(propertyColumnValue);
                ETWMonitorMode.RemoveResultsByName(propertyColumnValue);
                ReloadGridViews();
            }
        }

        private void ToolStripMenuItem2_FilterPID(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            FilterSelectedProperty(FilterPropertiesEnum.PID, out string? propertyColumnValue);
            bool isValid = int.TryParse(propertyColumnValue, out int integerValue);
            if (!string.IsNullOrEmpty(propertyColumnValue) && isValid)
            {
                BurstMode.RemoveResultsByPID(integerValue);
                ETWMonitorMode.RemoveResultsByPID(integerValue);
                ReloadGridViews();
            }
        }

        private void ToolStripMenuItem3_FilterInternalName(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            FilterSelectedProperty(FilterPropertiesEnum.InternalName, out string? propertyColumnValue);
            if (!string.IsNullOrEmpty(propertyColumnValue))
            {
                BurstMode.RemoveResultsByInternalName(propertyColumnValue);
                ETWMonitorMode.RemoveResultsByInternalName(propertyColumnValue);
                ReloadGridViews();
            }
        }

        private void ToolStripMenuItem8_FilterExecutablePath(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            FilterSelectedProperty(FilterPropertiesEnum.ExecutablePath, out string? propertyColumnValue);
            if (!string.IsNullOrEmpty(propertyColumnValue))
            {
                BurstMode.RemoveResultsByExecutablePath(propertyColumnValue);
                ETWMonitorMode.RemoveResultsByExecutablePath(propertyColumnValue);
                ReloadGridViews();
            }
        }

        private void ToolStripMenuItem4_OpenProperties(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            string? propertyColumnValue = GetPropertyColumnValue(FilterPropertiesEnum.ExecutablePath);
            if (!string.IsNullOrEmpty(propertyColumnValue))
            {
                FileProperties.ShowFileProperties(propertyColumnValue);
            }
        }

        private void ToolStripMenuItem_KillProcess(object sender, EventArgs e)
        {
            bool KillProcess(int pid, string executablePath)
            {
                bool result = false;
                if (pid == 0)
                {
                    return result;
                }

                try
                {
                    Process proc = Process.GetProcessById(pid);
                    if (proc != null
                        && proc.MainModule != null
                        && executablePath.Equals(proc.MainModule.FileName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        proc.Kill();
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch { }

                return result;
            }

            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            string? pidStr = GetPropertyColumnValue(FilterPropertiesEnum.PID);
            string? executablePath = GetPropertyColumnValue(FilterPropertiesEnum.ExecutablePath);
            if (!string.IsNullOrEmpty(pidStr) && !string.IsNullOrEmpty(executablePath))
            {
                if (KillProcess(int.Parse(pidStr), executablePath))
                {
                    SetInfoLabel_Killed(pidStr);
                }
                else
                {
                    SetInfoLabel_NoKilled(pidStr);
                }
            }
        }

        private void ToolStripMenuItem5_CopyProcessName(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            string? propertyColumnValue = GetPropertyColumnValue(FilterPropertiesEnum.Name);
            if (!string.IsNullOrEmpty(propertyColumnValue))
            {
                Clipboard.SetText(propertyColumnValue);
                SetInfoLabel_Copied();
            }
            else
            {
                SetInfoLabel_NoCopied();
            }
        }

        private void ToolStripMenuItem6_CopyExecutablePath(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            string? propertyColumnValue = GetPropertyColumnValue(FilterPropertiesEnum.ExecutablePath);
            if (!string.IsNullOrEmpty(propertyColumnValue))
            {
                Clipboard.SetText(propertyColumnValue);
                SetInfoLabel_Copied();
            }
            else
            {
                SetInfoLabel_NoCopied();
            }
        }

        private void ToolStripMenuItem10_CopyMD5Hash(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            string? propertyColumnValue = GetPropertyColumnValue("MD5");
            if (!string.IsNullOrEmpty(propertyColumnValue))
            {
                Clipboard.SetText(propertyColumnValue);
                SetInfoLabel_Copied();
            }
            else
            {
                SetInfoLabel_NoCopied();
            }
        }

        private void ToolStripMenuItem_CopyCommand(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            string? propertyColumnValue = GetPropertyColumnValue("Command");
            if (!string.IsNullOrEmpty(propertyColumnValue))
            {
                Clipboard.SetText(propertyColumnValue);
                SetInfoLabel_Copied();
            }
            else
            {
                SetInfoLabel_NoCopied();
            }
        }

        private void ToolStripMenuItem5_CopyPID(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            string? propertyColumnValue = GetPropertyColumnValue(FilterPropertiesEnum.PID);
            if (!string.IsNullOrEmpty(propertyColumnValue))
            {
                Clipboard.SetText(propertyColumnValue);
                SetInfoLabel_Copied();
            }
            else
            {
                SetInfoLabel_NoCopied();
            }
        }

        private void ToolStripMenuItem_RetrieveExecutableBackup(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            string? pidStr = GetPropertyColumnValue(FilterPropertiesEnum.PID);
            string? executablePath = GetPropertyColumnValue(FilterPropertiesEnum.ExecutablePath);
            if (string.IsNullOrEmpty(pidStr) || string.IsNullOrEmpty(executablePath))
            {
                return;
            }

            int pid = Convert.ToInt32(pidStr);
            string processKey = GetProcessKey(executablePath, pid);

            bool backupRetrieved = RetrieveExecutableBakupDialog(processKey);
            if (backupRetrieved)
            {
                SetInfoLabel_Backup(pidStr);
            }
            else
            {
                SetInfoLabel_NoBackup(pidStr);
            }
        }

        private bool RetrieveExecutableBakupDialog(string processKey)
        {
            bool backupRetrieved = false;
            string? executableBackupName = ExecutableBackupController.GetExecutableBackupName(processKey);
            if (string.IsNullOrEmpty(executableBackupName))
            {
                return false;
            }

            SaveFileDialog saveBackupDialog = new SaveFileDialog();

            saveBackupDialog.FileName = executableBackupName;
            saveBackupDialog.Filter = "Executable file (*.exe)|*.exe|All files (*.*)|*.*";
            saveBackupDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (saveBackupDialog.ShowDialog() == DialogResult.OK)
            {
                backupRetrieved = ExecutableBackupController.CopyExecutableBackup(processKey, saveBackupDialog.FileName);
            }

            return backupRetrieved;
        }

        private void ToolStripMenuItem_RetrieveDump(object sender, EventArgs e)
        {
            if (currentVisibleGridView == null
                || currentVisibleGridView.SelectedRows == null
                || currentVisibleGridView.SelectedRows.Count != 1)
            {
                return;
            }

            string? pidStr = GetPropertyColumnValue(FilterPropertiesEnum.PID);
            string? executablePath = GetPropertyColumnValue(FilterPropertiesEnum.ExecutablePath);
            if (string.IsNullOrEmpty(pidStr) || string.IsNullOrEmpty(executablePath))
            {
                return;
            }

            int pid = Convert.ToInt32(pidStr);
            bool isProcStillAlive = IsProcessStillAlive(pid, executablePath);
            bool isDumped = false;
            string processKey = GetProcessKey(executablePath, pid);
            if (isProcStillAlive)
            {
                isDumped = CreateDumpDialog(pid, processKey);
            }
            else
            {
                isDumped = RetrieveDumpDialog(processKey);
            }

            if (isDumped)
            {
                SetInfoLabel_Dumped(pidStr);
            }
            else
            {
                SetInfoLabel_NoDumped(pidStr);
            }
        }

        private SaveFileDialog GetSaveDumpDialog(string dumpName)
        {
            SaveFileDialog saveDumpDialog = new SaveFileDialog();

            saveDumpDialog.FileName = dumpName;
            saveDumpDialog.Filter = "Dump file (*.dmp)|*.dmp|All files (*.*)|*.*";
            saveDumpDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return saveDumpDialog;
        }

        private bool CreateDumpDialog(int pid, string processKey)
        {
            bool isDumped = false;

            DumpGenerator.WriteDumpForProcess(pid, processKey);
            string? dumpName = DumpGenerator.GetDumpName(processKey);
            if (string.IsNullOrEmpty(dumpName))
            {
                return false;
            }

            SaveFileDialog saveDumpDialog = GetSaveDumpDialog(dumpName);
            if (saveDumpDialog.ShowDialog() == DialogResult.OK)
            {
                isDumped = DumpGenerator.CopyDump(processKey, saveDumpDialog.FileName);
            }
            else
            {
                if (Properties.Settings.Default == null
                    || !Properties.Settings.Default.EnableAutomaticDump)
                {
                    DumpGenerator.DeleteDump(processKey);
                }
            }

            return isDumped;
        }

        private bool RetrieveDumpDialog(string processKey)
        {
            bool isDumped = false;

            string? dumpName = DumpGenerator.GetDumpName(processKey);
            if (string.IsNullOrEmpty(dumpName))
            {
                return false;
            }

            SaveFileDialog saveDumpDialog = GetSaveDumpDialog(dumpName);
            if (saveDumpDialog.ShowDialog() == DialogResult.OK)
            {
                isDumped = DumpGenerator.CopyDump(processKey, saveDumpDialog.FileName);
            }

            return isDumped;
        }

        #endregion

        #region UTILS

        private bool IsProcessStillAlive(int pid, string executablePath)
        {
            bool result = false;

            try
            {
                Process proc = Process.GetProcessById(pid);
                if (proc != null
                    && proc.MainModule != null
                    && executablePath.Equals(proc.MainModule.FileName, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch { }

            return result;
        }

        private string GetProcessKey(string executablePath, int pid)
        {
            return string.Format("{0}{1}", Path.GetFileName(executablePath), pid);
        }

        private string? GetPropertyColumnValue(FilterPropertiesEnum property)
        {
            return GetPropertyColumnValue(GetColumnIndexForProperty(property));
        }

        private string? GetPropertyColumnValue(string property)
        {
            return GetPropertyColumnValue(GetColumnIndexForProperty(property));
        }

        private string? GetPropertyColumnValue(int? rowIndex)
        {
            string propertyColumnValue = null;
            if (!rowIndex.HasValue)
            {
                return propertyColumnValue;
            }

            propertyColumnValue = currentVisibleGridView.SelectedRows[0].Cells[rowIndex.Value].Value.ToString();
            if (string.IsNullOrEmpty(propertyColumnValue))
            {
                return propertyColumnValue;
            }

            return propertyColumnValue;
        }

        private void FilterSelectedProperty(FilterPropertiesEnum property, out string? propertyColumnValue)
        {
            propertyColumnValue = GetPropertyColumnValue(property);
            if (propertyColumnValue != null)
            {
                FiltersController.AddFilter(property, FilterConditionsEnum.Is, propertyColumnValue);
                FiltersController.SaveFilters();
                UpdateFiltersButtonText();
            }
        }

        private int? GetColumnIndexForProperty(FilterPropertiesEnum property)
        {
            return GetColumnIndexForProperty(property.ToString());
        }

        private int? GetColumnIndexForProperty(string propertyStr)
        {
            foreach (DataGridViewColumn column in currentVisibleGridView.Columns)
            {
                if (column.HeaderText.Equals(propertyStr))
                {
                    return column.Index;
                }
            }

            return null;
        }

        #endregion

        #region INFO_LABEL
        private void SetInfoLabel_Copied()
        {
            SetInfoLabel("Copied!");
        }

        private void SetInfoLabel_NoCopied()
        {
            SetInfoLabel("Nothing to copy");
        }

        private void SetInfoLabel_Killed(string pid)
        {
            SetInfoLabel(string.Format("{0} killed!", pid));
        }

        private void SetInfoLabel_NoKilled(string pid)
        {
            SetInfoLabel(string.Format("The process {0} is no longer running", pid));
        }

        private void SetInfoLabel_Dumped(string pid)
        {
            SetInfoLabel(string.Format("{0} dumped!", pid));
        }

        private void SetInfoLabel_NoDumped(string pid)
        {
            SetInfoLabel(string.Format("No dump created for {0}", pid));
        }

        private void SetInfoLabel_Backup(string pid)
        {
            SetInfoLabel(string.Format("{0} executable backup retrieved!", pid));
        }

        private void SetInfoLabel_NoBackup(string pid)
        {
            SetInfoLabel(string.Format("No executable backup created for {0}", pid));
        }

        private void SetInfoLabel(string text)
        {
            InfoLabel.Show();
            InfoLabel.Text = text;

            InfoLabelTimer.Interval = 3000; //MS
            InfoLabelTimer.Tick += (s, e) =>
            {
                InfoLabel.Hide();
                InfoLabelTimer.Stop();
            };
            InfoLabelTimer.Start();
        }

        #endregion
    }
}
