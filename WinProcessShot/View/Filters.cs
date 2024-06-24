using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using WinProcessShot.Controller;
using WinProcessShot.Model;
using static System.Windows.Forms.DataFormats;

namespace WinProcessShot.View
{
    public partial class Filters : Form
    {
        private Main mainForm = null;

        public Filters(Form callingForm)
        {
            mainForm = callingForm as Main;
            InitializeComponent();
            LoadCurrentFilters();
        }

        private void LoadCurrentFilters()
        {
            if (FiltersController.CurrentFilters == null)
            {
                return;
            }

            foreach (Filter filter in FiltersController.CurrentFilters)
            {
                AddFilterToTable(
                    filter.FilterConfig.Property.ToString(), filter.FilterConfig.Condition.ToString(), filter.FilterConfig.Value.ToString()
                );
            }

            IncludeTrustedCheckBox.Checked = FiltersController.IncludeTrusted;
        }

        private void AddFilterToTable(string property, string condition, string value)
        {
            FiltersTable.Rows.Add(new string[] { property, condition, value });
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Filters_Load(object sender, EventArgs e)
        {
            ComboBoxProperty.DataSource = Enum.GetNames(typeof(FilterPropertiesEnum));
            ComboBoxCondition.DataSource = Enum.GetNames(typeof(FilterConditionsEnum));
        }

        private void FilterValue_TextChanged(object sender, EventArgs e)
        {
            ModifyAddButtonEnableStatus();
        }

        private void ComboBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyAddButtonEnableStatus();
        }

        private void ComboBoxProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyAddButtonEnableStatus();
        }

        private void ModifyAddButtonEnableStatus()
        {
            if (!string.IsNullOrEmpty(ComboBoxProperty.Text)
                && !string.IsNullOrEmpty(ComboBoxCondition.Text)
                && !string.IsNullOrEmpty(FilterValue.Text))
            {
                AddButton.Enabled = true;
            }
            else
            {
                AddButton.Enabled = false;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddFilterToTable(ComboBoxProperty.Text, ComboBoxCondition.Text, FilterValue.Text);
            ApplyButton.Enabled = true;
            FilterValue.Text = "";
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in FiltersTable.SelectedRows)
            {
                FiltersTable.Rows.RemoveAt(row.Index);
            }
            ApplyButton.Enabled = true;
        }

        private void FiltersTable_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (FiltersTable.SelectedRows.Count > 0)
            {
                RemoveButton.Enabled = true;
            }
            else
            {
                RemoveButton.Enabled = false;
            }
        }

        private void Apply()
        {
            FiltersController.ResetFilters();
            foreach (DataGridViewRow row in FiltersTable.Rows)
            {
                FiltersController.AddFilter(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
            }

            ApplyButton.Enabled = false;
            FiltersController.IncludeTrusted = IncludeTrustedCheckBox.Checked;
            FiltersController.SaveFilters();

            mainForm.UpdateFiltersButtonText();
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void Filters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S && ApplyButton.Enabled)
            {
                Apply();
            }
        }

        private void IncludeTrustedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ApplyButton.Enabled = true;
        }
    }

}
