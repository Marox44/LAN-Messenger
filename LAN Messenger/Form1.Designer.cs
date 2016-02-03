namespace LAN_Messenger
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLogWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu_notifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.listView_hosts = new ComponentOwl.BetterListView.BetterListView();
            this.imageList_hosts = new System.Windows.Forms.ImageList(this.components);
            this.betterListViewColumnHeader1 = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_send = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.lbl_selected = new System.Windows.Forms.Label();
            this.tb_send = new System.Windows.Forms.RichTextBox();
            this.panel_hostListLoading = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.loadingCircle_hostListLoading = new MRG.Controls.UI.LoadingCircle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_ip = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_tbCharsLeft = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.contextMenu_notifyIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listView_hosts)).BeginInit();
            this.panel_hostListLoading.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(734, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menu_exit
            // 
            this.menu_exit.Name = "menu_exit";
            this.menu_exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menu_exit.Size = new System.Drawing.Size(134, 22);
            this.menu_exit.Text = "Exit";
            this.menu_exit.Click += new System.EventHandler(this.menu_exit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(113, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.debugToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(106, 6);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLogWindowToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // showLogWindowToolStripMenuItem
            // 
            this.showLogWindowToolStripMenuItem.Name = "showLogWindowToolStripMenuItem";
            this.showLogWindowToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.showLogWindowToolStripMenuItem.Text = "Show log window";
            this.showLogWindowToolStripMenuItem.Click += new System.EventHandler(this.showLogWindowToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(106, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.ContextMenuStrip = this.contextMenu_notifyIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "LAN Messenger";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenu_notifyIcon
            // 
            this.contextMenu_notifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripMenuItem1});
            this.contextMenu_notifyIcon.Name = "contextMenu_notifyIcon";
            this.contextMenu_notifyIcon.Size = new System.Drawing.Size(93, 32);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(89, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem1.Text = "Exit";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // listView_hosts
            // 
            this.listView_hosts.DataBindColumns = true;
            this.listView_hosts.FontGroups = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listView_hosts.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.KeepSelection;
            this.listView_hosts.ImageList = this.imageList_hosts;
            this.listView_hosts.Location = new System.Drawing.Point(12, 74);
            this.listView_hosts.MultiSelect = false;
            this.listView_hosts.Name = "listView_hosts";
            this.listView_hosts.ShowToolTips = true;
            this.listView_hosts.Size = new System.Drawing.Size(232, 344);
            this.listView_hosts.TabIndex = 3;
            this.listView_hosts.SelectedIndexChanged += new System.EventHandler(this.listView_hosts_SelectedIndexChanged);
            this.listView_hosts.DataSourceChanged += new System.EventHandler(this.listView_hosts_DataSourceChanged);
            // 
            // imageList_hosts
            // 
            this.imageList_hosts.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_hosts.ImageStream")));
            this.imageList_hosts.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_hosts.Images.SetKeyName(0, "pc6.ico");
            // 
            // betterListViewColumnHeader1
            // 
            this.betterListViewColumnHeader1.AllowResize = false;
            this.betterListViewColumnHeader1.Name = "betterListViewColumnHeader1";
            this.betterListViewColumnHeader1.Text = "Hosts";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(71, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "establisher + statusChecker";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(276, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "listener";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(407, 452);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_send
            // 
            this.btn_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_send.Location = new System.Drawing.Point(618, 371);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(96, 47);
            this.btn_send.TabIndex = 7;
            this.btn_send.Text = "send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Visible = false;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(357, 27);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(160, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "receiver + parser";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(250, 74);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(464, 290);
            this.webBrowser1.TabIndex = 11;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.Visible = false;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // lbl_selected
            // 
            this.lbl_selected.AutoSize = true;
            this.lbl_selected.Location = new System.Drawing.Point(34, 371);
            this.lbl_selected.Name = "lbl_selected";
            this.lbl_selected.Size = new System.Drawing.Size(0, 13);
            this.lbl_selected.TabIndex = 12;
            // 
            // tb_send
            // 
            this.tb_send.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_send.DetectUrls = false;
            this.tb_send.Location = new System.Drawing.Point(250, 371);
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(362, 47);
            this.tb_send.TabIndex = 13;
            this.tb_send.Text = "";
            this.tb_send.Visible = false;
            this.tb_send.TextChanged += new System.EventHandler(this.tb_send_TextChanged);
            this.tb_send.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_send_KeyDown);
            this.tb_send.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_send_KeyUp);
            // 
            // panel_hostListLoading
            // 
            this.panel_hostListLoading.BackColor = System.Drawing.Color.White;
            this.panel_hostListLoading.Controls.Add(this.label1);
            this.panel_hostListLoading.Controls.Add(this.loadingCircle_hostListLoading);
            this.panel_hostListLoading.Location = new System.Drawing.Point(19, 158);
            this.panel_hostListLoading.Name = "panel_hostListLoading";
            this.panel_hostListLoading.Size = new System.Drawing.Size(219, 176);
            this.panel_hostListLoading.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(6, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Looking for hosts...";
            // 
            // loadingCircle_hostListLoading
            // 
            this.loadingCircle_hostListLoading.Active = true;
            this.loadingCircle_hostListLoading.Color = System.Drawing.Color.RoyalBlue;
            this.loadingCircle_hostListLoading.InnerCircleRadius = 8;
            this.loadingCircle_hostListLoading.Location = new System.Drawing.Point(52, 23);
            this.loadingCircle_hostListLoading.Name = "loadingCircle_hostListLoading";
            this.loadingCircle_hostListLoading.NumberSpoke = 24;
            this.loadingCircle_hostListLoading.OuterCircleRadius = 9;
            this.loadingCircle_hostListLoading.RotationSpeed = 40;
            this.loadingCircle_hostListLoading.Size = new System.Drawing.Size(108, 101);
            this.loadingCircle_hostListLoading.SpokeThickness = 4;
            this.loadingCircle_hostListLoading.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.IE7;
            this.loadingCircle_hostListLoading.TabIndex = 0;
            this.loadingCircle_hostListLoading.Text = "loadingCircle1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_ip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 485);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(734, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_ip
            // 
            this.lbl_ip.Name = "lbl_ip";
            this.lbl_ip.Size = new System.Drawing.Size(73, 17);
            this.lbl_ip.Text = "0.0.0.0:00000";
            // 
            // lbl_tbCharsLeft
            // 
            this.lbl_tbCharsLeft.AutoSize = true;
            this.lbl_tbCharsLeft.Location = new System.Drawing.Point(551, 421);
            this.lbl_tbCharsLeft.Name = "lbl_tbCharsLeft";
            this.lbl_tbCharsLeft.Size = new System.Drawing.Size(61, 13);
            this.lbl_tbCharsLeft.TabIndex = 17;
            this.lbl_tbCharsLeft.Text = "tbCharsLeft";
            this.lbl_tbCharsLeft.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 507);
            this.Controls.Add(this.lbl_tbCharsLeft);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel_hostListLoading);
            this.Controls.Add(this.tb_send);
            this.Controls.Add(this.lbl_selected);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView_hosts);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "LAN Messenger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenu_notifyIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listView_hosts)).EndInit();
            this.panel_hostListLoading.ResumeLayout(false);
            this.panel_hostListLoading.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_exit;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu_notifyIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private ComponentOwl.BetterListView.BetterListView listView_hosts;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader betterListViewColumnHeader1;
        private System.Windows.Forms.ImageList imageList_hosts;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLogWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label lbl_selected;
        private System.Windows.Forms.RichTextBox tb_send;
        private MRG.Controls.UI.LoadingCircle loadingCircle_hostListLoading;
        public System.Windows.Forms.Panel panel_hostListLoading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ip;
        private System.Windows.Forms.Label lbl_tbCharsLeft;

    }
}

