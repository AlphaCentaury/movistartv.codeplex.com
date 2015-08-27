namespace Project.DvbIpTv.UiServices.Discovery
{
    partial class UiBroadcastDiscoveryMergeResultDialog
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
            this.labelSuccess = new System.Windows.Forms.Label();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.labelBulletAdded = new System.Windows.Forms.Label();
            this.labelAdded = new System.Windows.Forms.Label();
            this.labelRemoved = new System.Windows.Forms.Label();
            this.labelBulletRemoved = new System.Windows.Forms.Label();
            this.labelChanged = new System.Windows.Forms.Label();
            this.labelBulletChanged = new System.Windows.Forms.Label();
            this.labelEqual = new System.Windows.Forms.Label();
            this.labelBulletEqual = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.pictureIconSuccess = new System.Windows.Forms.PictureBox();
            this.groupBoxResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconSuccess)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSuccess
            // 
            this.labelSuccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSuccess.Location = new System.Drawing.Point(42, 12);
            this.labelSuccess.Name = "labelSuccess";
            this.labelSuccess.Size = new System.Drawing.Size(330, 24);
            this.labelSuccess.TabIndex = 1;
            this.labelSuccess.Text = "(Success)";
            this.labelSuccess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxResults.Controls.Add(this.labelEqual);
            this.groupBoxResults.Controls.Add(this.labelBulletEqual);
            this.groupBoxResults.Controls.Add(this.labelChanged);
            this.groupBoxResults.Controls.Add(this.labelBulletChanged);
            this.groupBoxResults.Controls.Add(this.labelRemoved);
            this.groupBoxResults.Controls.Add(this.labelBulletRemoved);
            this.groupBoxResults.Controls.Add(this.labelAdded);
            this.groupBoxResults.Controls.Add(this.labelBulletAdded);
            this.groupBoxResults.Location = new System.Drawing.Point(12, 42);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(360, 108);
            this.groupBoxResults.TabIndex = 3;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "groupBox1";
            // 
            // labelBulletAdded
            // 
            this.labelBulletAdded.AutoSize = true;
            this.labelBulletAdded.Font = new System.Drawing.Font("Wingdings", 9F);
            this.labelBulletAdded.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelBulletAdded.Location = new System.Drawing.Point(6, 23);
            this.labelBulletAdded.Name = "labelBulletAdded";
            this.labelBulletAdded.Size = new System.Drawing.Size(16, 14);
            this.labelBulletAdded.TabIndex = 7;
            this.labelBulletAdded.Text = "l";
            // 
            // labelAdded
            // 
            this.labelAdded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAdded.AutoSize = true;
            this.labelAdded.Location = new System.Drawing.Point(28, 23);
            this.labelAdded.Margin = new System.Windows.Forms.Padding(3);
            this.labelAdded.Name = "labelAdded";
            this.labelAdded.Size = new System.Drawing.Size(43, 13);
            this.labelAdded.TabIndex = 4;
            this.labelAdded.Text = "(added)";
            // 
            // labelRemoved
            // 
            this.labelRemoved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRemoved.AutoSize = true;
            this.labelRemoved.Location = new System.Drawing.Point(28, 42);
            this.labelRemoved.Margin = new System.Windows.Forms.Padding(3);
            this.labelRemoved.Name = "labelRemoved";
            this.labelRemoved.Size = new System.Drawing.Size(54, 13);
            this.labelRemoved.TabIndex = 8;
            this.labelRemoved.Text = "(removed)";
            // 
            // labelBulletRemoved
            // 
            this.labelBulletRemoved.AutoSize = true;
            this.labelBulletRemoved.Font = new System.Drawing.Font("Wingdings", 9F);
            this.labelBulletRemoved.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelBulletRemoved.Location = new System.Drawing.Point(6, 42);
            this.labelBulletRemoved.Name = "labelBulletRemoved";
            this.labelBulletRemoved.Size = new System.Drawing.Size(16, 14);
            this.labelBulletRemoved.TabIndex = 9;
            this.labelBulletRemoved.Text = "l";
            // 
            // labelChanged
            // 
            this.labelChanged.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelChanged.AutoSize = true;
            this.labelChanged.Location = new System.Drawing.Point(28, 61);
            this.labelChanged.Margin = new System.Windows.Forms.Padding(3);
            this.labelChanged.Name = "labelChanged";
            this.labelChanged.Size = new System.Drawing.Size(55, 13);
            this.labelChanged.TabIndex = 10;
            this.labelChanged.Text = "(changed)";
            // 
            // labelBulletChanged
            // 
            this.labelBulletChanged.AutoSize = true;
            this.labelBulletChanged.Font = new System.Drawing.Font("Wingdings", 9F);
            this.labelBulletChanged.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelBulletChanged.Location = new System.Drawing.Point(6, 61);
            this.labelBulletChanged.Name = "labelBulletChanged";
            this.labelBulletChanged.Size = new System.Drawing.Size(16, 14);
            this.labelBulletChanged.TabIndex = 11;
            this.labelBulletChanged.Text = "l";
            // 
            // labelEqual
            // 
            this.labelEqual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEqual.AutoSize = true;
            this.labelEqual.Location = new System.Drawing.Point(28, 80);
            this.labelEqual.Margin = new System.Windows.Forms.Padding(3);
            this.labelEqual.Name = "labelEqual";
            this.labelEqual.Size = new System.Drawing.Size(39, 13);
            this.labelEqual.TabIndex = 12;
            this.labelEqual.Text = "(equal)";
            // 
            // labelBulletEqual
            // 
            this.labelBulletEqual.AutoSize = true;
            this.labelBulletEqual.Font = new System.Drawing.Font("Wingdings", 9F);
            this.labelBulletEqual.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelBulletEqual.Location = new System.Drawing.Point(6, 80);
            this.labelBulletEqual.Name = "labelBulletEqual";
            this.labelBulletEqual.Size = new System.Drawing.Size(16, 14);
            this.labelBulletEqual.TabIndex = 13;
            this.labelBulletEqual.Text = "l";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Image = global::Project.DvbIpTv.UiServices.Discovery.Properties.Resources.Action_Ok_16x16;
            this.buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonClose.Location = new System.Drawing.Point(272, 156);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 25);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "&OK";
            this.buttonClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // pictureIconSuccess
            // 
            this.pictureIconSuccess.Image = global::Project.DvbIpTv.UiServices.Discovery.Properties.Resources.Status_Success_24x24;
            this.pictureIconSuccess.Location = new System.Drawing.Point(12, 12);
            this.pictureIconSuccess.Name = "pictureIconSuccess";
            this.pictureIconSuccess.Size = new System.Drawing.Size(24, 24);
            this.pictureIconSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureIconSuccess.TabIndex = 0;
            this.pictureIconSuccess.TabStop = false;
            // 
            // UiBroadcastDiscoveryMergeResultDialog
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 192);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.labelSuccess);
            this.Controls.Add(this.pictureIconSuccess);
            this.Name = "UiBroadcastDiscoveryMergeResultDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "(UiBroadcastDiscoveryMergeResultDialog)";
            this.Load += new System.EventHandler(this.UiBroadcastDiscoveryMergeResultDialog_Load);
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconSuccess)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureIconSuccess;
        private System.Windows.Forms.Label labelSuccess;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Label labelEqual;
        private System.Windows.Forms.Label labelBulletEqual;
        private System.Windows.Forms.Label labelChanged;
        private System.Windows.Forms.Label labelBulletChanged;
        private System.Windows.Forms.Label labelRemoved;
        private System.Windows.Forms.Label labelBulletRemoved;
        private System.Windows.Forms.Label labelAdded;
        private System.Windows.Forms.Label labelBulletAdded;
        private System.Windows.Forms.Button buttonClose;
    }
}