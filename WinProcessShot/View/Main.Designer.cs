using System.Windows.Forms;

namespace WinProcessShot
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveAsJSONToolStripMenuItem = new ToolStripMenuItem();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            TabPanel = new TabControl();
            tabPage1 = new TabPage();
            DataGridViewNewProcess = new DataGridView();
            ProcessCreatedSeenAt = new DataGridViewTextBoxColumn();
            ProcessCreatedName = new DataGridViewTextBoxColumn();
            ProcessCreatedPID = new DataGridViewTextBoxColumn();
            ProcessCreatedInternalName = new DataGridViewTextBoxColumn();
            ProcessCreatedExecutablePath = new DataGridViewTextBoxColumn();
            ProcessCreatedCompanyName = new DataGridViewTextBoxColumn();
            ProcessCreatedParent = new DataGridViewTextBoxColumn();
            MD5 = new DataGridViewTextBoxColumn();
            Trusted = new DataGridViewTextBoxColumn();
            ProcessCreatedCommand = new DataGridViewTextBoxColumn();
            ProcessCreatedBasePriority = new DataGridViewTextBoxColumn();
            ProcessCreatedBaseAddress = new DataGridViewTextBoxColumn();
            ProcessCreatedEntryPointAddress = new DataGridViewTextBoxColumn();
            ProcessCreatedModuleMemorySize = new DataGridViewTextBoxColumn();
            tabPage2 = new TabPage();
            DataGridViewFinishedProcess = new DataGridView();
            ProcessDeletedSeenAt = new DataGridViewTextBoxColumn();
            ProcessDeletedName = new DataGridViewTextBoxColumn();
            ProcessDeletedPID = new DataGridViewTextBoxColumn();
            ProcessDeletedInternalName = new DataGridViewTextBoxColumn();
            ProcessDeletedExecutablePath = new DataGridViewTextBoxColumn();
            ProcessDeletedCompanyName = new DataGridViewTextBoxColumn();
            ProcessDeletedParent = new DataGridViewTextBoxColumn();
            FinishedMD5 = new DataGridViewTextBoxColumn();
            FinishedTrusted = new DataGridViewTextBoxColumn();
            ProcessDeletedCommand = new DataGridViewTextBoxColumn();
            ProcessDeletedBasePriority = new DataGridViewTextBoxColumn();
            ProcessDeletedBaseAddress = new DataGridViewTextBoxColumn();
            ProcessDeletedEntryPointAddress = new DataGridViewTextBoxColumn();
            ProcessDeletedModuleMemorySize = new DataGridViewTextBoxColumn();
            tabPage3 = new TabPage();
            DataGridViewProcessChanged = new DataGridView();
            ProcessChangedPID = new DataGridViewTextBoxColumn();
            ProcessChangedSeenAt = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            ProcessChangedName = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            ProcessChangedInternalName = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            ProcessChangedExecutablePath = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            ProcessChangedCompanyName = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            ProcessChangedBasePriority = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            ProcessChangedBaseAddress = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            ProcessChangedEntryPointAddress = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            ProcessChangedModuleMemorySize = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            FinishedProcessLabel = new Label();
            CreatedProcessLabel = new Label();
            ProcessChangedLabel = new Label();
            ElapsedTimeTimer = new System.Windows.Forms.Timer(components);
            DisablePreviewTableCheckBox = new CheckBox();
            toolTip1 = new ToolTip(components);
            toolStrip1 = new ToolStrip();
            MonitorButton = new ToolStripButton();
            ShotButton = new ToolStripButton();
            BurstModeButton = new ToolStripButton();
            ElapsedTimeLabel = new ToolStripLabel();
            BurstModeMS = new ToolStripTextBox();
            toolStripLabel1 = new ToolStripLabel();
            toolStripSeparator1 = new ToolStripSeparator();
            FiltersButton = new ToolStripButton();
            ClearButton = new ToolStripButton();
            CellContextMenu = new ContextMenuStrip(components);
            extractDumpToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem_View_RetrieveExecutableBackup = new ToolStripMenuItem();
            toolStripMenuItem1_View_FilterName = new ToolStripMenuItem();
            toolStripMenuItem2_View_FilterPID = new ToolStripMenuItem();
            toolStripMenuItem3_View_FilterInternalName = new ToolStripMenuItem();
            toolStripMenuItem8_View_FilterExecutablePath = new ToolStripMenuItem();
            toolStripMenuItem9_View_CopyPID = new ToolStripMenuItem();
            toolStripMenuItem5_View_CopyProcessName = new ToolStripMenuItem();
            toolStripMenuItem6_View_CopyExecutablePath = new ToolStripMenuItem();
            toolStripMenuItem10_View_CopyMD5 = new ToolStripMenuItem();
            toolStripMenuItem_View_CopyCommand = new ToolStripMenuItem();
            toolStripMenuItem4_View_OpenProperties = new ToolStripMenuItem();
            toolStripMenuItem_View_KillProcess = new ToolStripMenuItem();
            InfoLabel = new Label();
            InfoLabelTimer = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            TabPanel.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewNewProcess).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewFinishedProcess).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewProcessChanged).BeginInit();
            toolStrip1.SuspendLayout();
            CellContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, optionsToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(984, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveAsJSONToolStripMenuItem, ExitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveAsJSONToolStripMenuItem
            // 
            saveAsJSONToolStripMenuItem.Name = "saveAsJSONToolStripMenuItem";
            saveAsJSONToolStripMenuItem.Size = new Size(143, 22);
            saveAsJSONToolStripMenuItem.Text = "Save as JSON";
            saveAsJSONToolStripMenuItem.Click += saveAsJSONToolStripMenuItem_Click;
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new Size(143, 22);
            ExitToolStripMenuItem.Text = "Exit";
            ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click_1;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            optionsToolStripMenuItem.Click += optionsToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // TabPanel
            // 
            TabPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TabPanel.Controls.Add(tabPage1);
            TabPanel.Controls.Add(tabPage2);
            TabPanel.Location = new Point(12, 87);
            TabPanel.Name = "TabPanel";
            TabPanel.SelectedIndex = 0;
            TabPanel.Size = new Size(960, 484);
            TabPanel.TabIndex = 5;
            TabPanel.SelectedIndexChanged += TabPanel_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(DataGridViewNewProcess);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(952, 456);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Details New Processes";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // DataGridViewNewProcess
            // 
            DataGridViewNewProcess.AllowUserToAddRows = false;
            DataGridViewNewProcess.AllowUserToOrderColumns = true;
            DataGridViewNewProcess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewNewProcess.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewNewProcess.Columns.AddRange(new DataGridViewColumn[] { ProcessCreatedSeenAt, ProcessCreatedName, ProcessCreatedPID, ProcessCreatedInternalName, ProcessCreatedExecutablePath, ProcessCreatedCompanyName, ProcessCreatedParent, MD5, Trusted, ProcessCreatedCommand, ProcessCreatedBasePriority, ProcessCreatedBaseAddress, ProcessCreatedEntryPointAddress, ProcessCreatedModuleMemorySize });
            DataGridViewNewProcess.Dock = DockStyle.Fill;
            DataGridViewNewProcess.Location = new Point(3, 3);
            DataGridViewNewProcess.Name = "DataGridViewNewProcess";
            DataGridViewNewProcess.ReadOnly = true;
            DataGridViewNewProcess.Size = new Size(946, 450);
            DataGridViewNewProcess.TabIndex = 0;
            DataGridViewNewProcess.CellMouseDown += DataGridViewNewProcesses_CellMouseDown;
            // 
            // ProcessCreatedSeenAt
            // 
            ProcessCreatedSeenAt.HeaderText = "SeenAt";
            ProcessCreatedSeenAt.Name = "ProcessCreatedSeenAt";
            ProcessCreatedSeenAt.ReadOnly = true;
            // 
            // ProcessCreatedName
            // 
            ProcessCreatedName.HeaderText = "Name";
            ProcessCreatedName.Name = "ProcessCreatedName";
            ProcessCreatedName.ReadOnly = true;
            // 
            // ProcessCreatedPID
            // 
            ProcessCreatedPID.HeaderText = "PID";
            ProcessCreatedPID.Name = "ProcessCreatedPID";
            ProcessCreatedPID.ReadOnly = true;
            // 
            // ProcessCreatedInternalName
            // 
            ProcessCreatedInternalName.HeaderText = "InternalName";
            ProcessCreatedInternalName.Name = "ProcessCreatedInternalName";
            ProcessCreatedInternalName.ReadOnly = true;
            // 
            // ProcessCreatedExecutablePath
            // 
            ProcessCreatedExecutablePath.HeaderText = "ExecutablePath";
            ProcessCreatedExecutablePath.Name = "ProcessCreatedExecutablePath";
            ProcessCreatedExecutablePath.ReadOnly = true;
            // 
            // ProcessCreatedCompanyName
            // 
            ProcessCreatedCompanyName.HeaderText = "CompanyName";
            ProcessCreatedCompanyName.Name = "ProcessCreatedCompanyName";
            ProcessCreatedCompanyName.ReadOnly = true;
            // 
            // ProcessCreatedParent
            // 
            ProcessCreatedParent.HeaderText = "Parent";
            ProcessCreatedParent.Name = "ProcessCreatedParent";
            ProcessCreatedParent.ReadOnly = true;
            // 
            // MD5
            // 
            MD5.HeaderText = "MD5";
            MD5.Name = "MD5";
            MD5.ReadOnly = true;
            // 
            // Trusted
            // 
            Trusted.HeaderText = "Trusted";
            Trusted.Name = "Trusted";
            Trusted.ReadOnly = true;
            // 
            // ProcessCreatedCommand
            // 
            ProcessCreatedCommand.HeaderText = "Command";
            ProcessCreatedCommand.Name = "ProcessCreatedCommand";
            ProcessCreatedCommand.ReadOnly = true;
            // 
            // ProcessCreatedBasePriority
            // 
            ProcessCreatedBasePriority.HeaderText = "BasePriority";
            ProcessCreatedBasePriority.Name = "ProcessCreatedBasePriority";
            ProcessCreatedBasePriority.ReadOnly = true;
            // 
            // ProcessCreatedBaseAddress
            // 
            ProcessCreatedBaseAddress.HeaderText = "BaseAddress";
            ProcessCreatedBaseAddress.Name = "ProcessCreatedBaseAddress";
            ProcessCreatedBaseAddress.ReadOnly = true;
            // 
            // ProcessCreatedEntryPointAddress
            // 
            ProcessCreatedEntryPointAddress.HeaderText = "EntryPointAddress";
            ProcessCreatedEntryPointAddress.Name = "ProcessCreatedEntryPointAddress";
            ProcessCreatedEntryPointAddress.ReadOnly = true;
            // 
            // ProcessCreatedModuleMemorySize
            // 
            ProcessCreatedModuleMemorySize.HeaderText = "ModuleMemorySize";
            ProcessCreatedModuleMemorySize.Name = "ProcessCreatedModuleMemorySize";
            ProcessCreatedModuleMemorySize.ReadOnly = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(DataGridViewFinishedProcess);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(952, 456);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Details Finished Processes";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // DataGridViewFinishedProcess
            // 
            DataGridViewFinishedProcess.AllowUserToAddRows = false;
            DataGridViewFinishedProcess.AllowUserToOrderColumns = true;
            DataGridViewFinishedProcess.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewFinishedProcess.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewFinishedProcess.Columns.AddRange(new DataGridViewColumn[] { ProcessDeletedSeenAt, ProcessDeletedName, ProcessDeletedPID, ProcessDeletedInternalName, ProcessDeletedExecutablePath, ProcessDeletedCompanyName, ProcessDeletedParent, FinishedMD5, FinishedTrusted, ProcessDeletedCommand, ProcessDeletedBasePriority, ProcessDeletedBaseAddress, ProcessDeletedEntryPointAddress, ProcessDeletedModuleMemorySize });
            DataGridViewFinishedProcess.Dock = DockStyle.Fill;
            DataGridViewFinishedProcess.Location = new Point(3, 3);
            DataGridViewFinishedProcess.Name = "DataGridViewFinishedProcess";
            DataGridViewFinishedProcess.ReadOnly = true;
            DataGridViewFinishedProcess.Size = new Size(946, 450);
            DataGridViewFinishedProcess.TabIndex = 1;
            DataGridViewFinishedProcess.CellMouseDown += DataGridViewFinishedProcess_CellMouseDown;
            // 
            // ProcessDeletedSeenAt
            // 
            ProcessDeletedSeenAt.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ProcessDeletedSeenAt.Frozen = true;
            ProcessDeletedSeenAt.HeaderText = "SeenAt";
            ProcessDeletedSeenAt.Name = "ProcessDeletedSeenAt";
            ProcessDeletedSeenAt.ReadOnly = true;
            ProcessDeletedSeenAt.Width = 69;
            // 
            // ProcessDeletedName
            // 
            ProcessDeletedName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ProcessDeletedName.Frozen = true;
            ProcessDeletedName.HeaderText = "Name";
            ProcessDeletedName.Name = "ProcessDeletedName";
            ProcessDeletedName.ReadOnly = true;
            ProcessDeletedName.Width = 70;
            // 
            // ProcessDeletedPID
            // 
            ProcessDeletedPID.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ProcessDeletedPID.Frozen = true;
            ProcessDeletedPID.HeaderText = "PID";
            ProcessDeletedPID.Name = "ProcessDeletedPID";
            ProcessDeletedPID.ReadOnly = true;
            ProcessDeletedPID.Width = 69;
            // 
            // ProcessDeletedInternalName
            // 
            ProcessDeletedInternalName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ProcessDeletedInternalName.Frozen = true;
            ProcessDeletedInternalName.HeaderText = "InternalName";
            ProcessDeletedInternalName.Name = "ProcessDeletedInternalName";
            ProcessDeletedInternalName.ReadOnly = true;
            ProcessDeletedInternalName.Width = 70;
            // 
            // ProcessDeletedExecutablePath
            // 
            ProcessDeletedExecutablePath.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ProcessDeletedExecutablePath.Frozen = true;
            ProcessDeletedExecutablePath.HeaderText = "ExecutablePath";
            ProcessDeletedExecutablePath.Name = "ProcessDeletedExecutablePath";
            ProcessDeletedExecutablePath.ReadOnly = true;
            ProcessDeletedExecutablePath.Width = 69;
            // 
            // ProcessDeletedCompanyName
            // 
            ProcessDeletedCompanyName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ProcessDeletedCompanyName.Frozen = true;
            ProcessDeletedCompanyName.HeaderText = "CompanyName";
            ProcessDeletedCompanyName.Name = "ProcessDeletedCompanyName";
            ProcessDeletedCompanyName.ReadOnly = true;
            ProcessDeletedCompanyName.Width = 70;
            // 
            // ProcessDeletedParent
            // 
            ProcessDeletedParent.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ProcessDeletedParent.Frozen = true;
            ProcessDeletedParent.HeaderText = "Parent";
            ProcessDeletedParent.Name = "ProcessDeletedParent";
            ProcessDeletedParent.ReadOnly = true;
            // 
            // FinishedMD5
            // 
            FinishedMD5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FinishedMD5.Frozen = true;
            FinishedMD5.HeaderText = "MD5";
            FinishedMD5.Name = "FinishedMD5";
            FinishedMD5.ReadOnly = true;
            FinishedMD5.Width = 69;
            // 
            // FinishedTrusted
            // 
            FinishedTrusted.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FinishedTrusted.Frozen = true;
            FinishedTrusted.HeaderText = "Trusted";
            FinishedTrusted.Name = "FinishedTrusted";
            FinishedTrusted.ReadOnly = true;
            FinishedTrusted.Width = 70;
            // 
            // ProcessDeletedCommand
            // 
            ProcessDeletedCommand.HeaderText = "Command";
            ProcessDeletedCommand.Name = "ProcessDeletedCommand";
            ProcessDeletedCommand.ReadOnly = true;
            // 
            // ProcessDeletedBasePriority
            // 
            ProcessDeletedBasePriority.HeaderText = "BasePriority";
            ProcessDeletedBasePriority.Name = "ProcessDeletedBasePriority";
            ProcessDeletedBasePriority.ReadOnly = true;
            // 
            // ProcessDeletedBaseAddress
            // 
            ProcessDeletedBaseAddress.HeaderText = "BaseAddress";
            ProcessDeletedBaseAddress.Name = "ProcessDeletedBaseAddress";
            ProcessDeletedBaseAddress.ReadOnly = true;
            // 
            // ProcessDeletedEntryPointAddress
            // 
            ProcessDeletedEntryPointAddress.HeaderText = "EntryPointAddress";
            ProcessDeletedEntryPointAddress.Name = "ProcessDeletedEntryPointAddress";
            ProcessDeletedEntryPointAddress.ReadOnly = true;
            // 
            // ProcessDeletedModuleMemorySize
            // 
            ProcessDeletedModuleMemorySize.HeaderText = "ModuleMemorySize";
            ProcessDeletedModuleMemorySize.Name = "ProcessDeletedModuleMemorySize";
            ProcessDeletedModuleMemorySize.ReadOnly = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(DataGridViewProcessChanged);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(952, 456);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Details Changed Processes";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // DataGridViewProcessChanged
            // 
            DataGridViewProcessChanged.AllowUserToAddRows = false;
            DataGridViewProcessChanged.AllowUserToOrderColumns = true;
            DataGridViewProcessChanged.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewProcessChanged.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewProcessChanged.Columns.AddRange(new DataGridViewColumn[] { ProcessChangedPID, ProcessChangedSeenAt, Column9, ProcessChangedName, Column1, ProcessChangedInternalName, Column2, ProcessChangedExecutablePath, Column3, ProcessChangedCompanyName, Column4, ProcessChangedBasePriority, Column5, ProcessChangedBaseAddress, Column6, ProcessChangedEntryPointAddress, Column7, ProcessChangedModuleMemorySize, Column8 });
            DataGridViewProcessChanged.Dock = DockStyle.Fill;
            DataGridViewProcessChanged.Location = new Point(3, 3);
            DataGridViewProcessChanged.Name = "DataGridViewProcessChanged";
            DataGridViewProcessChanged.ReadOnly = true;
            DataGridViewProcessChanged.Size = new Size(946, 450);
            DataGridViewProcessChanged.TabIndex = 1;
            DataGridViewProcessChanged.CellMouseDown += DataGridViewProcessChanged_CellMouseDown;
            // 
            // ProcessChangedPID
            // 
            ProcessChangedPID.HeaderText = "PID";
            ProcessChangedPID.Name = "ProcessChangedPID";
            ProcessChangedPID.ReadOnly = true;
            // 
            // ProcessChangedSeenAt
            // 
            ProcessChangedSeenAt.HeaderText = "SeenProcessAt";
            ProcessChangedSeenAt.Name = "ProcessChangedSeenAt";
            ProcessChangedSeenAt.ReadOnly = true;
            // 
            // Column9
            // 
            Column9.HeaderText = "SeenProcessChangedAt";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            // 
            // ProcessChangedName
            // 
            ProcessChangedName.HeaderText = "Name.Before";
            ProcessChangedName.Name = "ProcessChangedName";
            ProcessChangedName.ReadOnly = true;
            // 
            // Column1
            // 
            Column1.HeaderText = "Name.After";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // ProcessChangedInternalName
            // 
            ProcessChangedInternalName.HeaderText = "InternalName.Before";
            ProcessChangedInternalName.Name = "ProcessChangedInternalName";
            ProcessChangedInternalName.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "InternalName.After";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // ProcessChangedExecutablePath
            // 
            ProcessChangedExecutablePath.HeaderText = "ExecutablePath.Before";
            ProcessChangedExecutablePath.Name = "ProcessChangedExecutablePath";
            ProcessChangedExecutablePath.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "ExecutablePath.After";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // ProcessChangedCompanyName
            // 
            ProcessChangedCompanyName.HeaderText = "CompanyName.Before";
            ProcessChangedCompanyName.Name = "ProcessChangedCompanyName";
            ProcessChangedCompanyName.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.HeaderText = "CompanyName.After";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // ProcessChangedBasePriority
            // 
            ProcessChangedBasePriority.HeaderText = "BasePriority.Before";
            ProcessChangedBasePriority.Name = "ProcessChangedBasePriority";
            ProcessChangedBasePriority.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.HeaderText = "BasePriority.After";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // ProcessChangedBaseAddress
            // 
            ProcessChangedBaseAddress.HeaderText = "BaseAddress.Before";
            ProcessChangedBaseAddress.Name = "ProcessChangedBaseAddress";
            ProcessChangedBaseAddress.ReadOnly = true;
            // 
            // Column6
            // 
            Column6.HeaderText = "BaseAddress.After";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // ProcessChangedEntryPointAddress
            // 
            ProcessChangedEntryPointAddress.HeaderText = "EntryPointAddress.Before";
            ProcessChangedEntryPointAddress.Name = "ProcessChangedEntryPointAddress";
            ProcessChangedEntryPointAddress.ReadOnly = true;
            // 
            // Column7
            // 
            Column7.HeaderText = "EntryPointAddress.After";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            // 
            // ProcessChangedModuleMemorySize
            // 
            ProcessChangedModuleMemorySize.HeaderText = "ModuleMemorySize.Before";
            ProcessChangedModuleMemorySize.Name = "ProcessChangedModuleMemorySize";
            ProcessChangedModuleMemorySize.ReadOnly = true;
            // 
            // Column8
            // 
            Column8.HeaderText = "ModuleMemorySize.After";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 59);
            label1.Name = "label1";
            label1.Size = new Size(159, 15);
            label1.TabIndex = 6;
            label1.Text = "Total new processes created: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(218, 59);
            label2.Name = "label2";
            label2.Size = new Size(137, 15);
            label2.TabIndex = 7;
            label2.Text = "Total finished processes: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(406, 59);
            label3.Name = "label3";
            label3.Size = new Size(165, 15);
            label3.TabIndex = 8;
            label3.Text = "Total processes with changes: ";
            label3.Visible = false;
            // 
            // FinishedProcessLabel
            // 
            FinishedProcessLabel.AutoSize = true;
            FinishedProcessLabel.Location = new Point(351, 59);
            FinishedProcessLabel.Name = "FinishedProcessLabel";
            FinishedProcessLabel.Size = new Size(13, 15);
            FinishedProcessLabel.TabIndex = 10;
            FinishedProcessLabel.Text = "0";
            // 
            // CreatedProcessLabel
            // 
            CreatedProcessLabel.AutoSize = true;
            CreatedProcessLabel.Location = new Point(163, 59);
            CreatedProcessLabel.Name = "CreatedProcessLabel";
            CreatedProcessLabel.Size = new Size(13, 15);
            CreatedProcessLabel.TabIndex = 11;
            CreatedProcessLabel.Text = "0";
            // 
            // ProcessChangedLabel
            // 
            ProcessChangedLabel.AutoSize = true;
            ProcessChangedLabel.Location = new Point(567, 59);
            ProcessChangedLabel.Name = "ProcessChangedLabel";
            ProcessChangedLabel.Size = new Size(13, 15);
            ProcessChangedLabel.TabIndex = 12;
            ProcessChangedLabel.Text = "0";
            ProcessChangedLabel.Visible = false;
            // 
            // ElapsedTimeTimer
            // 
            ElapsedTimeTimer.Interval = 10;
            ElapsedTimeTimer.Tick += ElapsedTimeTimer_Tick;
            // 
            // DisablePreviewTableCheckBox
            // 
            DisablePreviewTableCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DisablePreviewTableCheckBox.AutoSize = true;
            DisablePreviewTableCheckBox.Location = new Point(16, 577);
            DisablePreviewTableCheckBox.Name = "DisablePreviewTableCheckBox";
            DisablePreviewTableCheckBox.Size = new Size(137, 19);
            DisablePreviewTableCheckBox.TabIndex = 15;
            DisablePreviewTableCheckBox.Text = "Disable preview table";
            DisablePreviewTableCheckBox.UseVisualStyleBackColor = true;
            DisablePreviewTableCheckBox.CheckedChanged += DisablePreviewTableCheckBox_CheckedChanged;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { MonitorButton, ShotButton, BurstModeButton, ElapsedTimeLabel, BurstModeMS, toolStripLabel1, toolStripSeparator1, FiltersButton, ClearButton });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(984, 25);
            toolStrip1.TabIndex = 16;
            toolStrip1.Text = "toolStrip1";
            // 
            // MonitorButton
            // 
            MonitorButton.Image = Properties.Resources.monitor_icon;
            MonitorButton.ImageTransparentColor = Color.Magenta;
            MonitorButton.Name = "MonitorButton";
            MonitorButton.Size = new Size(70, 22);
            MonitorButton.Text = "Monitor";
            MonitorButton.ToolTipText = "Monitor Mode";
            MonitorButton.Click += MonitorModeButton_Click;
            // 
            // ShotButton
            // 
            ShotButton.Image = Properties.Resources.shot_icon;
            ShotButton.ImageTransparentColor = Color.Magenta;
            ShotButton.Name = "ShotButton";
            ShotButton.Size = new Size(68, 22);
            ShotButton.Text = "1st shot";
            ShotButton.Click += FirstShotButton_Click;
            // 
            // BurstModeButton
            // 
            BurstModeButton.Image = (Image)resources.GetObject("BurstModeButton.Image");
            BurstModeButton.ImageTransparentColor = Color.Magenta;
            BurstModeButton.Name = "BurstModeButton";
            BurstModeButton.Size = new Size(88, 22);
            BurstModeButton.Text = "Burst mode";
            BurstModeButton.ToolTipText = "Burst Mode";
            BurstModeButton.Click += BurstModeButton_Click;
            // 
            // ElapsedTimeLabel
            // 
            ElapsedTimeLabel.Alignment = ToolStripItemAlignment.Right;
            ElapsedTimeLabel.Margin = new Padding(0, 1, 4, 2);
            ElapsedTimeLabel.Name = "ElapsedTimeLabel";
            ElapsedTimeLabel.Size = new Size(122, 22);
            ElapsedTimeLabel.Text = "Elapsed time: 00:00:00";
            // 
            // BurstModeMS
            // 
            BurstModeMS.MaxLength = 6;
            BurstModeMS.Name = "BurstModeMS";
            BurstModeMS.Size = new Size(50, 25);
            BurstModeMS.Text = "1500";
            BurstModeMS.KeyPress += BurstModeM_KeyPress;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(24, 22);
            toolStripLabel1.Text = "MS";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // FiltersButton
            // 
            FiltersButton.Image = Properties.Resources.filter;
            FiltersButton.ImageTransparentColor = Color.Magenta;
            FiltersButton.Name = "FiltersButton";
            FiltersButton.Size = new Size(58, 22);
            FiltersButton.Text = "Filters";
            FiltersButton.Click += FiltersButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ClearButton.Enabled = false;
            ClearButton.Image = Properties.Resources.bin;
            ClearButton.ImageTransparentColor = Color.Magenta;
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(23, 22);
            ClearButton.Text = "Clear";
            ClearButton.Click += ClearShotButton_Click;
            // 
            // CellContextMenu
            // 
            CellContextMenu.Items.AddRange(new ToolStripItem[] { extractDumpToolStripMenuItem, toolStripMenuItem_View_RetrieveExecutableBackup, toolStripMenuItem1_View_FilterName, toolStripMenuItem2_View_FilterPID, toolStripMenuItem3_View_FilterInternalName, toolStripMenuItem8_View_FilterExecutablePath, toolStripMenuItem9_View_CopyPID, toolStripMenuItem5_View_CopyProcessName, toolStripMenuItem6_View_CopyExecutablePath, toolStripMenuItem10_View_CopyMD5, toolStripMenuItem_View_CopyCommand, toolStripMenuItem4_View_OpenProperties, toolStripMenuItem_View_KillProcess });
            CellContextMenu.Name = "CellConextMenu";
            CellContextMenu.Size = new Size(279, 290);
            // 
            // extractDumpToolStripMenuItem
            // 
            extractDumpToolStripMenuItem.Name = "extractDumpToolStripMenuItem";
            extractDumpToolStripMenuItem.Size = new Size(278, 22);
            extractDumpToolStripMenuItem.Text = "Create/Retrieve dump...";
            extractDumpToolStripMenuItem.Click += ToolStripMenuItem_RetrieveDump;
            // 
            // toolStripMenuItem_View_RetrieveExecutableBackup
            // 
            toolStripMenuItem_View_RetrieveExecutableBackup.Name = "toolStripMenuItem_View_RetrieveExecutableBackup";
            toolStripMenuItem_View_RetrieveExecutableBackup.Size = new Size(278, 22);
            toolStripMenuItem_View_RetrieveExecutableBackup.Text = "Retrieve executable backup...";
            toolStripMenuItem_View_RetrieveExecutableBackup.Click += ToolStripMenuItem_RetrieveExecutableBackup;
            // 
            // toolStripMenuItem1_View_FilterName
            // 
            toolStripMenuItem1_View_FilterName.Name = "toolStripMenuItem1_View_FilterName";
            toolStripMenuItem1_View_FilterName.Size = new Size(278, 22);
            toolStripMenuItem1_View_FilterName.Text = "Filter process with this name";
            toolStripMenuItem1_View_FilterName.Click += ToolStripMenuItem1_FilterName;
            // 
            // toolStripMenuItem2_View_FilterPID
            // 
            toolStripMenuItem2_View_FilterPID.Name = "toolStripMenuItem2_View_FilterPID";
            toolStripMenuItem2_View_FilterPID.Size = new Size(278, 22);
            toolStripMenuItem2_View_FilterPID.Text = "Filter process with this PID";
            toolStripMenuItem2_View_FilterPID.Click += ToolStripMenuItem2_FilterPID;
            // 
            // toolStripMenuItem3_View_FilterInternalName
            // 
            toolStripMenuItem3_View_FilterInternalName.Name = "toolStripMenuItem3_View_FilterInternalName";
            toolStripMenuItem3_View_FilterInternalName.Size = new Size(278, 22);
            toolStripMenuItem3_View_FilterInternalName.Text = "Filter process with this internal name";
            toolStripMenuItem3_View_FilterInternalName.Click += ToolStripMenuItem3_FilterInternalName;
            // 
            // toolStripMenuItem8_View_FilterExecutablePath
            // 
            toolStripMenuItem8_View_FilterExecutablePath.Name = "toolStripMenuItem8_View_FilterExecutablePath";
            toolStripMenuItem8_View_FilterExecutablePath.Size = new Size(278, 22);
            toolStripMenuItem8_View_FilterExecutablePath.Text = "Filter process with this executable path";
            toolStripMenuItem8_View_FilterExecutablePath.Click += ToolStripMenuItem8_FilterExecutablePath;
            // 
            // toolStripMenuItem9_View_CopyPID
            // 
            toolStripMenuItem9_View_CopyPID.Name = "toolStripMenuItem9_View_CopyPID";
            toolStripMenuItem9_View_CopyPID.Size = new Size(278, 22);
            toolStripMenuItem9_View_CopyPID.Text = "Copy process PID";
            toolStripMenuItem9_View_CopyPID.Click += ToolStripMenuItem5_CopyPID;
            // 
            // toolStripMenuItem5_View_CopyProcessName
            // 
            toolStripMenuItem5_View_CopyProcessName.Name = "toolStripMenuItem5_View_CopyProcessName";
            toolStripMenuItem5_View_CopyProcessName.Size = new Size(278, 22);
            toolStripMenuItem5_View_CopyProcessName.Text = "Copy process name";
            toolStripMenuItem5_View_CopyProcessName.Click += ToolStripMenuItem5_CopyProcessName;
            // 
            // toolStripMenuItem6_View_CopyExecutablePath
            // 
            toolStripMenuItem6_View_CopyExecutablePath.Name = "toolStripMenuItem6_View_CopyExecutablePath";
            toolStripMenuItem6_View_CopyExecutablePath.Size = new Size(278, 22);
            toolStripMenuItem6_View_CopyExecutablePath.Text = "Copy process executable path";
            toolStripMenuItem6_View_CopyExecutablePath.Click += ToolStripMenuItem6_CopyExecutablePath;
            // 
            // toolStripMenuItem10_View_CopyMD5
            // 
            toolStripMenuItem10_View_CopyMD5.Name = "toolStripMenuItem10_View_CopyMD5";
            toolStripMenuItem10_View_CopyMD5.Size = new Size(278, 22);
            toolStripMenuItem10_View_CopyMD5.Text = "Copy MD5 hash";
            toolStripMenuItem10_View_CopyMD5.Click += ToolStripMenuItem10_CopyMD5Hash;
            // 
            // toolStripMenuItem_View_CopyCommand
            // 
            toolStripMenuItem_View_CopyCommand.Name = "toolStripMenuItem_View_CopyCommand";
            toolStripMenuItem_View_CopyCommand.Size = new Size(278, 22);
            toolStripMenuItem_View_CopyCommand.Text = "Copy command";
            toolStripMenuItem_View_CopyCommand.Click += ToolStripMenuItem_CopyCommand;
            // 
            // toolStripMenuItem4_View_OpenProperties
            // 
            toolStripMenuItem4_View_OpenProperties.Name = "toolStripMenuItem4_View_OpenProperties";
            toolStripMenuItem4_View_OpenProperties.Size = new Size(278, 22);
            toolStripMenuItem4_View_OpenProperties.Text = "Properties";
            toolStripMenuItem4_View_OpenProperties.Click += ToolStripMenuItem4_OpenProperties;
            // 
            // toolStripMenuItem_View_KillProcess
            // 
            toolStripMenuItem_View_KillProcess.Name = "toolStripMenuItem_View_KillProcess";
            toolStripMenuItem_View_KillProcess.Size = new Size(278, 22);
            toolStripMenuItem_View_KillProcess.Text = "Kill process";
            toolStripMenuItem_View_KillProcess.Click += ToolStripMenuItem_KillProcess;
            // 
            // InfoLabel
            // 
            InfoLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            InfoLabel.ForeColor = Color.Maroon;
            InfoLabel.Location = new Point(668, 577);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(300, 19);
            InfoLabel.TabIndex = 17;
            InfoLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 608);
            Controls.Add(InfoLabel);
            Controls.Add(toolStrip1);
            Controls.Add(DisablePreviewTableCheckBox);
            Controls.Add(ProcessChangedLabel);
            Controls.Add(CreatedProcessLabel);
            Controls.Add(FinishedProcessLabel);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TabPanel);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(724, 621);
            Name = "Main";
            Text = "WinProcessShot";
            Load += Form1_Load;
            KeyDown += Main_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            TabPanel.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewNewProcess).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewFinishedProcess).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataGridViewProcessChanged).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            CellContextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveAsJSONToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private TabControl TabPanel;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label FinishedProcessLabel;
        private Label CreatedProcessLabel;
        private Label ProcessChangedLabel;
        private System.Windows.Forms.Timer ElapsedTimeTimer;
        private DataGridView DataGridViewNewProcess;
        private CheckBox DisablePreviewTableCheckBox;
        private DataGridView DataGridViewFinishedProcess;
        private DataGridView DataGridViewProcessChanged;
        private DataGridViewTextBoxColumn ProcessChangedPID;
        private DataGridViewTextBoxColumn ProcessChangedSeenAt;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn ProcessChangedName;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn ProcessChangedInternalName;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn ProcessChangedExecutablePath;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn ProcessChangedCompanyName;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn ProcessChangedBasePriority;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn ProcessChangedBaseAddress;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn ProcessChangedEntryPointAddress;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn ProcessChangedModuleMemorySize;
        private DataGridViewTextBoxColumn Column8;
        private ToolTip toolTip1;
        private ToolStrip toolStrip1;
        private ToolStripButton BurstModeButton;
        private ToolStripButton ShotButton;
        private ToolStripLabel ElapsedTimeLabel;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton ClearButton;
        private ToolStripButton FiltersButton;
        private ContextMenuStrip CellContextMenu;
        private ToolStripMenuItem toolStripMenuItem1_View_FilterName;
        private ToolStripMenuItem toolStripMenuItem2_View_FilterPID;
        private ToolStripMenuItem toolStripMenuItem3_View_FilterInternalName;
        private ToolStripMenuItem toolStripMenuItem4_View_OpenProperties;
        private ToolStripMenuItem toolStripMenuItem5_View_CopyProcessName;
        private ToolStripMenuItem toolStripMenuItem6_View_CopyExecutablePath;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8_View_FilterExecutablePath;
        private ToolStripTextBox BurstModeMS;
        private ToolStripLabel toolStripLabel1;
        private ToolStripMenuItem toolStripMenuItem9_View_CopyPID;
        private ToolStripMenuItem toolStripMenuItem10_View_CopyMD5;
        private ToolStripButton MonitorButton;
        private ToolStripMenuItem toolStripMenuItem_View_KillProcess;
        private ToolStripMenuItem toolStripMenuItem_View_CopyCommand;
        private DataGridViewTextBoxColumn ProcessDeletedSeenAt;
        private DataGridViewTextBoxColumn ProcessDeletedName;
        private DataGridViewTextBoxColumn ProcessDeletedPID;
        private DataGridViewTextBoxColumn ProcessDeletedInternalName;
        private DataGridViewTextBoxColumn ProcessDeletedExecutablePath;
        private DataGridViewTextBoxColumn ProcessDeletedCompanyName;
        private DataGridViewTextBoxColumn ProcessDeletedParent;
        private DataGridViewTextBoxColumn FinishedMD5;
        private DataGridViewTextBoxColumn FinishedTrusted;
        private DataGridViewTextBoxColumn ProcessDeletedCommand;
        private DataGridViewTextBoxColumn ProcessDeletedBasePriority;
        private DataGridViewTextBoxColumn ProcessDeletedBaseAddress;
        private DataGridViewTextBoxColumn ProcessDeletedEntryPointAddress;
        private DataGridViewTextBoxColumn ProcessDeletedModuleMemorySize;
        private DataGridViewTextBoxColumn ProcessCreatedSeenAt;
        private DataGridViewTextBoxColumn ProcessCreatedName;
        private DataGridViewTextBoxColumn ProcessCreatedPID;
        private DataGridViewTextBoxColumn ProcessCreatedInternalName;
        private DataGridViewTextBoxColumn ProcessCreatedExecutablePath;
        private DataGridViewTextBoxColumn ProcessCreatedCompanyName;
        private DataGridViewTextBoxColumn ProcessCreatedParent;
        private DataGridViewTextBoxColumn MD5;
        private DataGridViewTextBoxColumn Trusted;
        private DataGridViewTextBoxColumn ProcessCreatedCommand;
        private DataGridViewTextBoxColumn ProcessCreatedBasePriority;
        private DataGridViewTextBoxColumn ProcessCreatedBaseAddress;
        private DataGridViewTextBoxColumn ProcessCreatedEntryPointAddress;
        private DataGridViewTextBoxColumn ProcessCreatedModuleMemorySize;
        private Label InfoLabel;
        private System.Windows.Forms.Timer InfoLabelTimer;
        private ToolStripMenuItem extractDumpToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem_View_RetrieveExecutableBackup;
    }
}