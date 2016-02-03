using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LAN_Messenger
{
    public partial class Form_log : Form
    {
        public Form_log()
        {
            InitializeComponent();
            this.LOG.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(delegate(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) { if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) { this.refreshLog(); } });
        }
        private ObservableCollection<string> LOG = new ObservableCollection<string>();

        public void addLog(string text)
        {
            this.LOG.Add(DateTime.Now.ToString("HH:mm:ss tt") + " \t " + text);
        }

        private void refreshLog()
        {
            if (string.IsNullOrWhiteSpace(this.tb_searchBar.Text))
            {
                this.listBox_eventLog.BeginUpdate();
                this.listBox_eventLog.Items.Clear();
                this.listBox_eventLog.Items.AddRange(this.LOG.ToArray());
                if (LOG.Count > 5)
                {
                    this.listBox_eventLog.SelectedIndex = this.listBox_eventLog.Items.Count - 1;
                    this.listBox_eventLog.SelectedIndex = -1;
                }
                this.listBox_eventLog.EndUpdate();
            }
            else
            {
                this.tb_searchBar_TextChanged(null, null);
            }
        }

        private void Form_log_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void tb_searchBar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.tb_searchBar.Text))
            {
                this.listBox_eventLog.BeginUpdate();

                this.listBox_eventLog.Items.Clear();
                foreach (string str in this.LOG)
                {
                    //if (str.Contains(this.tb_searchBar.Text))
                    //{
                    //    this.listBox_eventLog.Items.Add(str);
                    //}
                    if (str.IndexOf(this.tb_searchBar.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        this.listBox_eventLog.Items.Add(str);
                    }
                }

                this.listBox_eventLog.EndUpdate();
            }
            else
            {
                this.refreshLog();
            }
        }
    }
}
