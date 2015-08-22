namespace Project.DvbIpTv.ChannelList
{
    partial class TestNewChannelListForm
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
            this.listViewChannelList = new Project.DvbIpTv.UiServices.Common.Controls.ListViewSortable();
            this.comboListMode = new System.Windows.Forms.ComboBox();
            this.buttonApplyMode = new System.Windows.Forms.Button();
            this.checkShowGridlines = new System.Windows.Forms.CheckBox();
            this.comboColumns = new System.Windows.Forms.ComboBox();
            this.buttonNullSelection = new System.Windows.Forms.Button();
            this.buttonSort = new System.Windows.Forms.Button();
            this.comboSortDirection = new System.Windows.Forms.ComboBox();
            this.buttonConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewChannelList
            // 
            this.listViewChannelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewChannelList.HeaderCustomFont = null;
            this.listViewChannelList.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewChannelList.IsDoubleBuffered = true;
            this.listViewChannelList.Location = new System.Drawing.Point(12, 12);
            this.listViewChannelList.Name = "listViewChannelList";
            this.listViewChannelList.Size = new System.Drawing.Size(700, 340);
            this.listViewChannelList.TabIndex = 0;
            this.listViewChannelList.UseCompatibleStateImageBehavior = false;
            // 
            // comboListMode
            // 
            this.comboListMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboListMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboListMode.FormattingEnabled = true;
            this.comboListMode.Items.AddRange(new object[] {
            "0: Large",
            "1: Detail",
            "2: Small",
            "3: List",
            "4: Tile"});
            this.comboListMode.Location = new System.Drawing.Point(12, 358);
            this.comboListMode.Name = "comboListMode";
            this.comboListMode.Size = new System.Drawing.Size(121, 21);
            this.comboListMode.TabIndex = 1;
            // 
            // buttonApplyMode
            // 
            this.buttonApplyMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonApplyMode.Location = new System.Drawing.Point(12, 385);
            this.buttonApplyMode.Name = "buttonApplyMode";
            this.buttonApplyMode.Size = new System.Drawing.Size(100, 25);
            this.buttonApplyMode.TabIndex = 2;
            this.buttonApplyMode.Text = "Set mode";
            this.buttonApplyMode.UseVisualStyleBackColor = true;
            this.buttonApplyMode.Click += new System.EventHandler(this.buttonApplyMode_Click);
            // 
            // checkShowGridlines
            // 
            this.checkShowGridlines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkShowGridlines.AutoSize = true;
            this.checkShowGridlines.Location = new System.Drawing.Point(157, 360);
            this.checkShowGridlines.Name = "checkShowGridlines";
            this.checkShowGridlines.Size = new System.Drawing.Size(69, 17);
            this.checkShowGridlines.TabIndex = 3;
            this.checkShowGridlines.Text = "Grid lines";
            this.checkShowGridlines.UseVisualStyleBackColor = true;
            this.checkShowGridlines.CheckedChanged += new System.EventHandler(this.checkShowGridlines_CheckedChanged);
            // 
            // comboColumns
            // 
            this.comboColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboColumns.FormattingEnabled = true;
            this.comboColumns.Location = new System.Drawing.Point(283, 358);
            this.comboColumns.Name = "comboColumns";
            this.comboColumns.Size = new System.Drawing.Size(185, 21);
            this.comboColumns.TabIndex = 4;
            // 
            // buttonNullSelection
            // 
            this.buttonNullSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNullSelection.Location = new System.Drawing.Point(157, 385);
            this.buttonNullSelection.Name = "buttonNullSelection";
            this.buttonNullSelection.Size = new System.Drawing.Size(100, 25);
            this.buttonNullSelection.TabIndex = 5;
            this.buttonNullSelection.Text = "Remove selection";
            this.buttonNullSelection.UseVisualStyleBackColor = true;
            this.buttonNullSelection.Click += new System.EventHandler(this.buttonNullSelection_Click);
            // 
            // buttonSort
            // 
            this.buttonSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSort.Location = new System.Drawing.Point(368, 385);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(100, 25);
            this.buttonSort.TabIndex = 6;
            this.buttonSort.Text = "Sort list";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // comboSortDirection
            // 
            this.comboSortDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboSortDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSortDirection.FormattingEnabled = true;
            this.comboSortDirection.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.comboSortDirection.Location = new System.Drawing.Point(283, 388);
            this.comboSortDirection.Name = "comboSortDirection";
            this.comboSortDirection.Size = new System.Drawing.Size(79, 21);
            this.comboSortDirection.TabIndex = 7;
            // 
            // buttonConfig
            // 
            this.buttonConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonConfig.Location = new System.Drawing.Point(612, 385);
            this.buttonConfig.Name = "buttonConfig";
            this.buttonConfig.Size = new System.Drawing.Size(100, 25);
            this.buttonConfig.TabIndex = 8;
            this.buttonConfig.Text = "Edit settings...";
            this.buttonConfig.UseVisualStyleBackColor = true;
            this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // TestNewChannelListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 422);
            this.Controls.Add(this.buttonConfig);
            this.Controls.Add(this.comboSortDirection);
            this.Controls.Add(this.buttonSort);
            this.Controls.Add(this.buttonNullSelection);
            this.Controls.Add(this.comboColumns);
            this.Controls.Add(this.checkShowGridlines);
            this.Controls.Add(this.buttonApplyMode);
            this.Controls.Add(this.comboListMode);
            this.Controls.Add(this.listViewChannelList);
            this.Name = "TestNewChannelListForm";
            this.Text = "TestNewChannelListForm";
            this.Load += new System.EventHandler(this.TestNewChannelListForm_Load);
            this.Shown += new System.EventHandler(this.TestNewChannelListForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Project.DvbIpTv.UiServices.Common.Controls.ListViewSortable listViewChannelList;
        private System.Windows.Forms.ComboBox comboListMode;
        private System.Windows.Forms.Button buttonApplyMode;
        private System.Windows.Forms.CheckBox checkShowGridlines;
        private System.Windows.Forms.ComboBox comboColumns;
        private System.Windows.Forms.Button buttonNullSelection;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.ComboBox comboSortDirection;
        private System.Windows.Forms.Button buttonConfig;
    }
}