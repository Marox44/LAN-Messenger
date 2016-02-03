namespace LAN_Messenger
{
    partial class Form_log
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
            this.tb_searchBar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox_eventLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // tb_searchBar
            // 
            this.tb_searchBar.Location = new System.Drawing.Point(69, 10);
            this.tb_searchBar.Name = "tb_searchBar";
            this.tb_searchBar.Size = new System.Drawing.Size(399, 20);
            this.tb_searchBar.TabIndex = 1;
            this.tb_searchBar.TextChanged += new System.EventHandler(this.tb_searchBar_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search:";
            // 
            // listBox_eventLog
            // 
            this.listBox_eventLog.FormattingEnabled = true;
            this.listBox_eventLog.HorizontalScrollbar = true;
            this.listBox_eventLog.Location = new System.Drawing.Point(12, 38);
            this.listBox_eventLog.Name = "listBox_eventLog";
            this.listBox_eventLog.Size = new System.Drawing.Size(456, 264);
            this.listBox_eventLog.TabIndex = 0;
            // 
            // Form_log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 309);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_searchBar);
            this.Controls.Add(this.listBox_eventLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form_log";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Event log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_log_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_searchBar;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox listBox_eventLog;

    }
}