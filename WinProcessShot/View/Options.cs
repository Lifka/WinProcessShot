using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinProcessShot.Controller;

namespace WinProcessShot.View
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            TempDirectoryTextBox.Text = DumpGenerator.AppDataDirectory;
            if (Properties.Settings.Default == null)
            {
                return;
            }

            EnableAutomaticDumpCheckBox.Checked = Properties.Settings.Default.EnableAutomaticDump;
            EnableAutomaticExeBackupCheckBox.Checked = Properties.Settings.Default.EnableAutomaticExeBackup;
            ApplyButton.Enabled = false;
        }

        private void Apply()
        {
            Properties.Settings.Default.EnableAutomaticDump = EnableAutomaticDumpCheckBox.Checked;
            Properties.Settings.Default.EnableAutomaticExeBackup = EnableAutomaticExeBackupCheckBox.Checked;
            Properties.Settings.Default.AppDataDirectory = TempDirectoryTextBox.Text;
            DumpGenerator.AppDataDirectory = TempDirectoryTextBox.Text;

            Properties.Settings.Default.Save();

            ApplyButton.Enabled = false;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TempDirectoryTextBox_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = TempDirectoryTextBox.Text;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TempDirectoryTextBox.Text = dialog.SelectedPath;
            }

            if (!TempDirectoryTextBox.Text.Equals(dialog.InitialDirectory, StringComparison.InvariantCultureIgnoreCase))
            {
                ApplyButton.Enabled = true;
            }
        }

        private void EnableAutomaticDumpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ApplyButton.Enabled = true;
        }

        private void EnableAutomaticExeBackupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ApplyButton.Enabled = true;
        }
    }
}
