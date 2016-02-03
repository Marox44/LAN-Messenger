using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace LAN_Messenger
{
    public partial class Form_options : Form
    {
        public Form_options()
        {
            InitializeComponent();

            var ip_list = Network.Tools.getLocalIPAddresses();
            this.cb_localIP.DataSource = ip_list;
            this.cb_localIP.SelectedIndex = ip_list.FindIndex(d => d.ToString() == Properties.Settings.Default.LocalIP);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LocalIP = (this.cb_localIP.SelectedValue as IPAddress).ToString();
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
        }
    }
}
