namespace WinProcessShot.View
{
    partial class Filters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Filters));
            ComboBoxProperty = new ComboBox();
            ComboBoxCondition = new ComboBox();
            label1 = new Label();
            FilterValue = new TextBox();
            RemoveButton = new Button();
            AddButton = new Button();
            ApplyButton = new Button();
            CancelButton = new Button();
            FiltersTable = new DataGridView();
            Property = new DataGridViewTextBoxColumn();
            Condition = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            IncludeTrustedCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)FiltersTable).BeginInit();
            SuspendLayout();
            // 
            // ComboBoxProperty
            // 
            ComboBoxProperty.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxProperty.FormattingEnabled = true;
            ComboBoxProperty.Items.AddRange(new object[] { "Name", "InternalName", "ExecutablePath", "CompanyName", "PID" });
            ComboBoxProperty.Location = new Point(12, 72);
            ComboBoxProperty.Name = "ComboBoxProperty";
            ComboBoxProperty.Size = new Size(199, 23);
            ComboBoxProperty.TabIndex = 0;
            ComboBoxProperty.SelectedIndexChanged += ComboBoxProperty_SelectedIndexChanged;
            // 
            // ComboBoxCondition
            // 
            ComboBoxCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxCondition.FormattingEnabled = true;
            ComboBoxCondition.Items.AddRange(new object[] { "contains", "not contains", "is", "is not", "less than", "more than" });
            ComboBoxCondition.Location = new Point(217, 72);
            ComboBoxCondition.Name = "ComboBoxCondition";
            ComboBoxCondition.Size = new Size(92, 23);
            ComboBoxCondition.TabIndex = 1;
            ComboBoxCondition.SelectedIndexChanged += ComboBoxCondition_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 49);
            label1.Name = "label1";
            label1.Size = new Size(319, 15);
            label1.TabIndex = 2;
            label1.Text = "Processes that meet these conditions will NOT be collected:";
            // 
            // FilterValue
            // 
            FilterValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FilterValue.Location = new Point(315, 72);
            FilterValue.Name = "FilterValue";
            FilterValue.Size = new Size(203, 23);
            FilterValue.TabIndex = 3;
            FilterValue.TextChanged += FilterValue_TextChanged;
            // 
            // RemoveButton
            // 
            RemoveButton.Enabled = false;
            RemoveButton.Location = new Point(12, 110);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(90, 23);
            RemoveButton.TabIndex = 5;
            RemoveButton.Text = "Remove";
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // AddButton
            // 
            AddButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            AddButton.Enabled = false;
            AddButton.Location = new Point(428, 110);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(90, 23);
            AddButton.TabIndex = 6;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // ApplyButton
            // 
            ApplyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ApplyButton.Enabled = false;
            ApplyButton.Location = new Point(428, 459);
            ApplyButton.Name = "ApplyButton";
            ApplyButton.Size = new Size(90, 23);
            ApplyButton.TabIndex = 7;
            ApplyButton.Text = "Apply";
            ApplyButton.UseVisualStyleBackColor = true;
            ApplyButton.Click += ApplyButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CancelButton.Location = new Point(329, 459);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(90, 23);
            CancelButton.TabIndex = 8;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // FiltersTable
            // 
            FiltersTable.AllowUserToAddRows = false;
            FiltersTable.AllowUserToDeleteRows = false;
            FiltersTable.AllowUserToResizeRows = false;
            FiltersTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FiltersTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FiltersTable.Columns.AddRange(new DataGridViewColumn[] { Property, Condition, Value });
            FiltersTable.Location = new Point(12, 148);
            FiltersTable.Name = "FiltersTable";
            FiltersTable.ReadOnly = true;
            FiltersTable.Size = new Size(506, 305);
            FiltersTable.TabIndex = 9;
            FiltersTable.RowStateChanged += FiltersTable_RowStateChanged;
            // 
            // Property
            // 
            Property.FillWeight = 64.51613F;
            Property.HeaderText = "Property";
            Property.Name = "Property";
            Property.ReadOnly = true;
            // 
            // Condition
            // 
            Condition.FillWeight = 102.740074F;
            Condition.HeaderText = "Condition";
            Condition.Name = "Condition";
            Condition.ReadOnly = true;
            Condition.Width = 159;
            // 
            // Value
            // 
            Value.FillWeight = 132.7438F;
            Value.HeaderText = "Value";
            Value.Name = "Value";
            Value.ReadOnly = true;
            Value.Width = 206;
            // 
            // IncludeTrustedCheckBox
            // 
            IncludeTrustedCheckBox.AutoSize = true;
            IncludeTrustedCheckBox.Checked = true;
            IncludeTrustedCheckBox.CheckState = CheckState.Checked;
            IncludeTrustedCheckBox.Location = new Point(12, 12);
            IncludeTrustedCheckBox.Name = "IncludeTrustedCheckBox";
            IncludeTrustedCheckBox.Size = new Size(379, 19);
            IncludeTrustedCheckBox.TabIndex = 10;
            IncludeTrustedCheckBox.Text = "Include trusted processes (trusted = valid signature or known hash)";
            IncludeTrustedCheckBox.UseVisualStyleBackColor = true;
            IncludeTrustedCheckBox.CheckedChanged += IncludeTrustedCheckBox_CheckedChanged;
            // 
            // Filters
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(527, 494);
            Controls.Add(IncludeTrustedCheckBox);
            Controls.Add(FiltersTable);
            Controls.Add(CancelButton);
            Controls.Add(ApplyButton);
            Controls.Add(AddButton);
            Controls.Add(RemoveButton);
            Controls.Add(FilterValue);
            Controls.Add(label1);
            Controls.Add(ComboBoxCondition);
            Controls.Add(ComboBoxProperty);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MinimumSize = new Size(543, 465);
            Name = "Filters";
            Text = "Filters";
            Load += Filters_Load;
            KeyDown += Filters_KeyDown;
            ((System.ComponentModel.ISupportInitialize)FiltersTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ComboBoxProperty;
        private ComboBox ComboBoxCondition;
        private Label label1;
        private TextBox FilterValue;
        private Button ResetButton;
        private Button RemoveButton;
        private Button AddButton;
        private Button ApplyButton;
        private Button CancelButton;
        private DataGridView FiltersTable;
        private DataGridViewTextBoxColumn Property;
        private DataGridViewTextBoxColumn Condition;
        private DataGridViewTextBoxColumn Value;
        private CheckBox IncludeTrustedCheckBox;
    }
}