namespace WinProcessShot.View
{
    partial class Options
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
            CancelButton = new Button();
            ApplyButton = new Button();
            EnableAutomaticDumpCheckBox = new CheckBox();
            EnableAutomaticExeBackupCheckBox = new CheckBox();
            TempDirectoryTextBox = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // CancelButton
            // 
            CancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CancelButton.Location = new Point(271, 189);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(90, 23);
            CancelButton.TabIndex = 10;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // ApplyButton
            // 
            ApplyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ApplyButton.Enabled = false;
            ApplyButton.Location = new Point(370, 189);
            ApplyButton.Name = "ApplyButton";
            ApplyButton.Size = new Size(90, 23);
            ApplyButton.TabIndex = 9;
            ApplyButton.Text = "Apply";
            ApplyButton.UseVisualStyleBackColor = true;
            ApplyButton.Click += ApplyButton_Click;
            // 
            // EnableAutomaticDumpCheckBox
            // 
            EnableAutomaticDumpCheckBox.AutoSize = true;
            EnableAutomaticDumpCheckBox.Location = new Point(12, 71);
            EnableAutomaticDumpCheckBox.Name = "EnableAutomaticDumpCheckBox";
            EnableAutomaticDumpCheckBox.Size = new Size(276, 34);
            EnableAutomaticDumpCheckBox.TabIndex = 11;
            EnableAutomaticDumpCheckBox.Text = "Keep an automatic dump of the new processes \r\n(if the process dies, you can recover the dump).";
            EnableAutomaticDumpCheckBox.UseVisualStyleBackColor = true;
            EnableAutomaticDumpCheckBox.CheckedChanged += EnableAutomaticDumpCheckBox_CheckedChanged;
            // 
            // EnableAutomaticExeBackupCheckBox
            // 
            EnableAutomaticExeBackupCheckBox.AutoSize = true;
            EnableAutomaticExeBackupCheckBox.Location = new Point(12, 111);
            EnableAutomaticExeBackupCheckBox.Name = "EnableAutomaticExeBackupCheckBox";
            EnableAutomaticExeBackupCheckBox.Size = new Size(417, 34);
            EnableAutomaticExeBackupCheckBox.TabIndex = 12;
            EnableAutomaticExeBackupCheckBox.Text = "Maintain an automatic copy of the executables that create processes \r\n(if the executable disappears, you will have a copy to be able to analyze it).";
            EnableAutomaticExeBackupCheckBox.UseVisualStyleBackColor = true;
            EnableAutomaticExeBackupCheckBox.CheckedChanged += EnableAutomaticExeBackupCheckBox_CheckedChanged;
            // 
            // TempDirectoryTextBox
            // 
            TempDirectoryTextBox.Location = new Point(12, 33);
            TempDirectoryTextBox.Name = "TempDirectoryTextBox";
            TempDirectoryTextBox.Size = new Size(447, 23);
            TempDirectoryTextBox.TabIndex = 13;
            TempDirectoryTextBox.Click += TempDirectoryTextBox_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 15);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 14;
            label1.Text = "Temp directory:";
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(472, 224);
            Controls.Add(label1);
            Controls.Add(TempDirectoryTextBox);
            Controls.Add(EnableAutomaticExeBackupCheckBox);
            Controls.Add(EnableAutomaticDumpCheckBox);
            Controls.Add(CancelButton);
            Controls.Add(ApplyButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Options";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Options";
            Load += Options_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CancelButton;
        private Button ApplyButton;
        private CheckBox EnableAutomaticDumpCheckBox;
        private CheckBox EnableAutomaticExeBackupCheckBox;
        private TextBox TempDirectoryTextBox;
        private Label label1;
    }
}