namespace Project.DvbIpTv.UiServices.Discovery
{
    partial class SettingsEditorSorting
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupSortBy = new System.Windows.Forms.GroupBox();
            this.checkGlobalSorting = new System.Windows.Forms.CheckBox();
            this.buttonThirdDirection = new System.Windows.Forms.Button();
            this.comboThirdColumn = new System.Windows.Forms.ComboBox();
            this.buttonSecondDirection = new System.Windows.Forms.Button();
            this.comboSecondColumn = new System.Windows.Forms.ComboBox();
            this.buttonFirstDirection = new System.Windows.Forms.Button();
            this.comboFirstColumn = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupSortBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupSortBy
            // 
            this.groupSortBy.Controls.Add(this.checkGlobalSorting);
            this.groupSortBy.Controls.Add(this.buttonThirdDirection);
            this.groupSortBy.Controls.Add(this.comboThirdColumn);
            this.groupSortBy.Controls.Add(this.buttonSecondDirection);
            this.groupSortBy.Controls.Add(this.comboSecondColumn);
            this.groupSortBy.Controls.Add(this.buttonFirstDirection);
            this.groupSortBy.Controls.Add(this.comboFirstColumn);
            this.groupSortBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSortBy.Location = new System.Drawing.Point(0, 0);
            this.groupSortBy.Name = "groupSortBy";
            this.groupSortBy.Size = new System.Drawing.Size(225, 130);
            this.groupSortBy.TabIndex = 10;
            this.groupSortBy.TabStop = false;
            this.groupSortBy.Text = "Sort list by";
            // 
            // checkGlobalSorting
            // 
            this.checkGlobalSorting.AutoSize = true;
            this.checkGlobalSorting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkGlobalSorting.Location = new System.Drawing.Point(6, 100);
            this.checkGlobalSorting.Name = "checkGlobalSorting";
            this.checkGlobalSorting.Size = new System.Drawing.Size(97, 17);
            this.checkGlobalSorting.TabIndex = 12;
            this.checkGlobalSorting.Text = "Global override";
            this.checkGlobalSorting.UseVisualStyleBackColor = true;
            // 
            // buttonThirdDirection
            // 
            this.buttonThirdDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonThirdDirection.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonThirdDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonThirdDirection.Image = global::Project.DvbIpTv.UiServices.Discovery.Properties.Resources.Action_SortAscending_16x16;
            this.buttonThirdDirection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonThirdDirection.Location = new System.Drawing.Point(192, 70);
            this.buttonThirdDirection.Name = "buttonThirdDirection";
            this.buttonThirdDirection.Size = new System.Drawing.Size(25, 25);
            this.buttonThirdDirection.TabIndex = 11;
            this.buttonThirdDirection.UseVisualStyleBackColor = true;
            this.buttonThirdDirection.Click += new System.EventHandler(this.buttonThirdDirection_Click);
            // 
            // comboThirdColumn
            // 
            this.comboThirdColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboThirdColumn.DisplayMember = "Value";
            this.comboThirdColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboThirdColumn.DropDownWidth = 250;
            this.comboThirdColumn.FormattingEnabled = true;
            this.comboThirdColumn.Location = new System.Drawing.Point(6, 73);
            this.comboThirdColumn.Name = "comboThirdColumn";
            this.comboThirdColumn.Size = new System.Drawing.Size(180, 21);
            this.comboThirdColumn.TabIndex = 10;
            this.comboThirdColumn.ValueMember = "Key";
            this.comboThirdColumn.SelectedIndexChanged += new System.EventHandler(this.comboThirdColumn_SelectedIndexChanged);
            // 
            // buttonSecondDirection
            // 
            this.buttonSecondDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSecondDirection.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonSecondDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSecondDirection.Image = global::Project.DvbIpTv.UiServices.Discovery.Properties.Resources.Action_SortAscending_16x16;
            this.buttonSecondDirection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSecondDirection.Location = new System.Drawing.Point(192, 43);
            this.buttonSecondDirection.Name = "buttonSecondDirection";
            this.buttonSecondDirection.Size = new System.Drawing.Size(25, 25);
            this.buttonSecondDirection.TabIndex = 9;
            this.buttonSecondDirection.UseVisualStyleBackColor = true;
            this.buttonSecondDirection.Click += new System.EventHandler(this.buttonSecondDirection_Click);
            // 
            // comboSecondColumn
            // 
            this.comboSecondColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSecondColumn.DisplayMember = "Value";
            this.comboSecondColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSecondColumn.DropDownWidth = 250;
            this.comboSecondColumn.FormattingEnabled = true;
            this.comboSecondColumn.Location = new System.Drawing.Point(6, 46);
            this.comboSecondColumn.Name = "comboSecondColumn";
            this.comboSecondColumn.Size = new System.Drawing.Size(180, 21);
            this.comboSecondColumn.TabIndex = 8;
            this.comboSecondColumn.ValueMember = "Key";
            this.comboSecondColumn.SelectedIndexChanged += new System.EventHandler(this.comboSecondColumn_SelectedIndexChanged);
            // 
            // buttonFirstDirection
            // 
            this.buttonFirstDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFirstDirection.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonFirstDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFirstDirection.Image = global::Project.DvbIpTv.UiServices.Discovery.Properties.Resources.Action_SortAscending_16x16;
            this.buttonFirstDirection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonFirstDirection.Location = new System.Drawing.Point(192, 16);
            this.buttonFirstDirection.Name = "buttonFirstDirection";
            this.buttonFirstDirection.Size = new System.Drawing.Size(25, 25);
            this.buttonFirstDirection.TabIndex = 7;
            this.buttonFirstDirection.UseVisualStyleBackColor = true;
            this.buttonFirstDirection.Click += new System.EventHandler(this.buttonFirstDirection_Click);
            // 
            // comboFirstColumn
            // 
            this.comboFirstColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboFirstColumn.DisplayMember = "Value";
            this.comboFirstColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFirstColumn.DropDownWidth = 250;
            this.comboFirstColumn.FormattingEnabled = true;
            this.comboFirstColumn.Location = new System.Drawing.Point(6, 19);
            this.comboFirstColumn.Name = "comboFirstColumn";
            this.comboFirstColumn.Size = new System.Drawing.Size(180, 21);
            this.comboFirstColumn.TabIndex = 6;
            this.comboFirstColumn.ValueMember = "Key";
            this.comboFirstColumn.SelectedIndexChanged += new System.EventHandler(this.comboFirstColumn_SelectedIndexChanged);
            // 
            // SettingsEditorSorting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupSortBy);
            this.Name = "SettingsEditorSorting";
            this.Size = new System.Drawing.Size(225, 130);
            this.Load += new System.EventHandler(this.SettingsEditorSorting_Load);
            this.groupSortBy.ResumeLayout(false);
            this.groupSortBy.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupSortBy;
        private System.Windows.Forms.Button buttonThirdDirection;
        private System.Windows.Forms.ComboBox comboThirdColumn;
        private System.Windows.Forms.Button buttonSecondDirection;
        private System.Windows.Forms.ComboBox comboSecondColumn;
        private System.Windows.Forms.Button buttonFirstDirection;
        private System.Windows.Forms.ComboBox comboFirstColumn;
        private System.Windows.Forms.CheckBox checkGlobalSorting;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
