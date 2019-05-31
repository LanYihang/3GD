namespace _3GD
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.lblftp = new System.Windows.Forms.Label();
            this.txtFTP = new System.Windows.Forms.TextBox();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.tvFTP = new System.Windows.Forms.TreeView();
            this.btnSpecialDown = new System.Windows.Forms.Button();
            this.cmstripDownload = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.下载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmstripDownload.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblftp
            // 
            this.lblftp.AutoSize = true;
            this.lblftp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblftp.Location = new System.Drawing.Point(32, 28);
            this.lblftp.Name = "lblftp";
            this.lblftp.Size = new System.Drawing.Size(119, 19);
            this.lblftp.TabIndex = 0;
            this.lblftp.Text = "FTP Server:";
            // 
            // txtFTP
            // 
            this.txtFTP.Enabled = false;
            this.txtFTP.Location = new System.Drawing.Point(157, 28);
            this.txtFTP.Name = "txtFTP";
            this.txtFTP.Size = new System.Drawing.Size(368, 21);
            this.txtFTP.TabIndex = 1;
            this.txtFTP.Text = "http://www.3gpp.org/ftp/";
            // 
            // txtShow
            // 
            this.txtShow.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShow.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtShow.Location = new System.Drawing.Point(336, 93);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.ReadOnly = true;
            this.txtShow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtShow.Size = new System.Drawing.Size(490, 341);
            this.txtShow.TabIndex = 2;
            // 
            // tvFTP
            // 
            this.tvFTP.Location = new System.Drawing.Point(36, 67);
            this.tvFTP.Name = "tvFTP";
            this.tvFTP.ShowNodeToolTips = true;
            this.tvFTP.Size = new System.Drawing.Size(270, 367);
            this.tvFTP.TabIndex = 3;
            this.tvFTP.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFTP_NodeMouseClick);
            this.tvFTP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvFTP_MouseUp);
            // 
            // btnSpecialDown
            // 
            this.btnSpecialDown.Location = new System.Drawing.Point(336, 67);
            this.btnSpecialDown.Name = "btnSpecialDown";
            this.btnSpecialDown.Size = new System.Drawing.Size(88, 23);
            this.btnSpecialDown.TabIndex = 4;
            this.btnSpecialDown.Text = "特定下载";
            this.btnSpecialDown.UseVisualStyleBackColor = true;
            this.btnSpecialDown.Click += new System.EventHandler(this.btnSpecialDown_Click);
            // 
            // cmstripDownload
            // 
            this.cmstripDownload.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下载ToolStripMenuItem});
            this.cmstripDownload.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.cmstripDownload.Name = "cmstripDownload";
            this.cmstripDownload.Size = new System.Drawing.Size(101, 26);
            this.cmstripDownload.Text = "下载";
            // 
            // 下载ToolStripMenuItem
            // 
            this.下载ToolStripMenuItem.Name = "下载ToolStripMenuItem";
            this.下载ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.下载ToolStripMenuItem.Text = "下载";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 468);
            this.Controls.Add(this.btnSpecialDown);
            this.Controls.Add(this.tvFTP);
            this.Controls.Add(this.txtShow);
            this.Controls.Add(this.txtFTP);
            this.Controls.Add(this.lblftp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "3GD";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.cmstripDownload.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblftp;
        private System.Windows.Forms.TextBox txtFTP;
        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.TreeView tvFTP;
        private System.Windows.Forms.Button btnSpecialDown;
        private System.Windows.Forms.ContextMenuStrip cmstripDownload;
        private System.Windows.Forms.ToolStripMenuItem 下载ToolStripMenuItem;
    }
}