namespace Project.DvbIpTv.UiServices.EPG
{
    partial class MovistarEpgInfoDialog
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
            this.components = new System.ComponentModel.Container();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.richTextProgramData = new System.Windows.Forms.RichTextBox();
            this.contextMenuRtf = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextRtfMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextRtfMenuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonRecordProgram = new System.Windows.Forms.Button();
            this.buttonShowProgram = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureChannelLogo = new System.Windows.Forms.PictureBox();
            this.buttonZoom = new System.Windows.Forms.Button();
            this.pictureProgramPreview = new System.Windows.Forms.PictureBox();
            this.contextMenuRtf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgramPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // labelChannelName
            // 
            this.labelChannelName.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.labelChannelName.Location = new System.Drawing.Point(82, 210);
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.Size = new System.Drawing.Size(185, 64);
            this.labelChannelName.TabIndex = 3;
            this.labelChannelName.Text = "(Channel #\r\nChannel name)";
            this.labelChannelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // richTextProgramData
            // 
            this.richTextProgramData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextProgramData.BackColor = System.Drawing.SystemColors.Window;
            this.richTextProgramData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextProgramData.ContextMenuStrip = this.contextMenuRtf;
            this.richTextProgramData.HideSelection = false;
            this.richTextProgramData.Location = new System.Drawing.Point(283, 12);
            this.richTextProgramData.Margin = new System.Windows.Forms.Padding(13, 3, 3, 3);
            this.richTextProgramData.Name = "richTextProgramData";
            this.richTextProgramData.ReadOnly = true;
            this.richTextProgramData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextProgramData.Size = new System.Drawing.Size(389, 357);
            this.richTextProgramData.TabIndex = 35;
            this.richTextProgramData.Text = "";
            // 
            // contextMenuRtf
            // 
            this.contextMenuRtf.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextRtfMenuCopy,
            this.contextRtfMenuSelectAll});
            this.contextMenuRtf.Name = "contextMenuRtf";
            this.contextMenuRtf.Size = new System.Drawing.Size(153, 48);
            // 
            // contextRtfMenuCopy
            // 
            this.contextRtfMenuCopy.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Copy_Clip_16x16;
            this.contextRtfMenuCopy.Name = "contextRtfMenuCopy";
            this.contextRtfMenuCopy.Size = new System.Drawing.Size(152, 22);
            this.contextRtfMenuCopy.Text = "Copy selection";
            this.contextRtfMenuCopy.Click += new System.EventHandler(this.contextRtfMenuCopy_Click);
            // 
            // contextRtfMenuSelectAll
            // 
            this.contextRtfMenuSelectAll.Name = "contextRtfMenuSelectAll";
            this.contextRtfMenuSelectAll.Size = new System.Drawing.Size(152, 22);
            this.contextRtfMenuSelectAll.Text = "Select all";
            this.contextRtfMenuSelectAll.Click += new System.EventHandler(this.contextRtfMenuSelectAll_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNext.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Forward_16x16;
            this.buttonNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonNext.Location = new System.Drawing.Point(118, 375);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(100, 25);
            this.buttonNext.TabIndex = 33;
            this.buttonNext.Text = "&Next";
            this.buttonNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPrevious.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Back_16x16;
            this.buttonPrevious.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonPrevious.Location = new System.Drawing.Point(12, 375);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(100, 25);
            this.buttonPrevious.TabIndex = 34;
            this.buttonPrevious.Text = "&Previous";
            this.buttonPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonRecordProgram
            // 
            this.buttonRecordProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRecordProgram.Enabled = false;
            this.buttonRecordProgram.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Record_16x16;
            this.buttonRecordProgram.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonRecordProgram.Location = new System.Drawing.Point(447, 375);
            this.buttonRecordProgram.Name = "buttonRecordProgram";
            this.buttonRecordProgram.Size = new System.Drawing.Size(100, 25);
            this.buttonRecordProgram.TabIndex = 31;
            this.buttonRecordProgram.Text = "Rec&ord...";
            this.buttonRecordProgram.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRecordProgram.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRecordProgram.UseVisualStyleBackColor = true;
            // 
            // buttonShowProgram
            // 
            this.buttonShowProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowProgram.Enabled = false;
            this.buttonShowProgram.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Play_LG_16x16;
            this.buttonShowProgram.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonShowProgram.Location = new System.Drawing.Point(341, 375);
            this.buttonShowProgram.Name = "buttonShowProgram";
            this.buttonShowProgram.Size = new System.Drawing.Size(100, 25);
            this.buttonShowProgram.TabIndex = 32;
            this.buttonShowProgram.Text = "&Show...";
            this.buttonShowProgram.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonShowProgram.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonShowProgram.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Ok_16x16;
            this.buttonOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOk.Location = new System.Drawing.Point(572, 375);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 25);
            this.buttonOk.TabIndex = 30;
            this.buttonOk.Text = "&Ok";
            this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureChannelLogo
            // 
            this.pictureChannelLogo.Location = new System.Drawing.Point(12, 210);
            this.pictureChannelLogo.Name = "pictureChannelLogo";
            this.pictureChannelLogo.Size = new System.Drawing.Size(64, 64);
            this.pictureChannelLogo.TabIndex = 2;
            this.pictureChannelLogo.TabStop = false;
            // 
            // buttonZoom
            // 
            this.buttonZoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonZoom.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_FullView_16x16;
            this.buttonZoom.Location = new System.Drawing.Point(247, 184);
            this.buttonZoom.Name = "buttonZoom";
            this.buttonZoom.Size = new System.Drawing.Size(20, 20);
            this.buttonZoom.TabIndex = 1;
            this.buttonZoom.UseVisualStyleBackColor = true;
            // 
            // pictureProgramPreview
            // 
            this.pictureProgramPreview.ErrorImage = global::Project.DvbIpTv.UiServices.EPG.Properties.Resources.EpgNoProgramImage;
            this.pictureProgramPreview.Image = global::Project.DvbIpTv.UiServices.EPG.Properties.Resources.EpgLoadingProgramImage;
            this.pictureProgramPreview.Location = new System.Drawing.Point(12, 12);
            this.pictureProgramPreview.Name = "pictureProgramPreview";
            this.pictureProgramPreview.Size = new System.Drawing.Size(255, 192);
            this.pictureProgramPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureProgramPreview.TabIndex = 0;
            this.pictureProgramPreview.TabStop = false;
            // 
            // MovistarEpgInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.richTextProgramData);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonRecordProgram);
            this.Controls.Add(this.buttonShowProgram);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.pictureChannelLogo);
            this.Controls.Add(this.buttonZoom);
            this.Controls.Add(this.pictureProgramPreview);
            this.MinimizeBox = false;
            this.Name = "MovistarEpgInfoDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MovistarEpgInfoDialog";
            this.Load += new System.EventHandler(this.MovistarEpgInfoDialog_Load);
            this.contextMenuRtf.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgramPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureProgramPreview;
        private System.Windows.Forms.Button buttonZoom;
        private System.Windows.Forms.PictureBox pictureChannelLogo;
        private System.Windows.Forms.Label labelChannelName;
        private System.Windows.Forms.Button buttonRecordProgram;
        private System.Windows.Forms.Button buttonShowProgram;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.RichTextBox richTextProgramData;
        private System.Windows.Forms.ContextMenuStrip contextMenuRtf;
        private System.Windows.Forms.ToolStripMenuItem contextRtfMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem contextRtfMenuSelectAll;
    }
}