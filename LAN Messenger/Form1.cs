using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Marox.ExtensionMethods;
using System.DirectoryServices;
using ComponentOwl.BetterListView;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Input;
using System.Threading;

namespace LAN_Messenger
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            //todo: 
            Marox.THREAD.CreateThreadFromFunction(() => { while (true) { this.lbl_ip.Text = Properties.Settings.Default.LocalIP + ":" + global.PORT.ToString(); Thread.Sleep(500); } }, "", true, true);


            //chatlog load from file
            //global.l_chatLog = ChatLogFile.readChatLogFromFile(global.ChatLogFileName);
            //chatlog list event
            global.l_chatLog.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(delegate(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) { if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) { this.ChatLog_Add(sender, e); } });
            //todo: zmiana ip w czasie pracy!

            global.p_main = this;
            global.p_Form_log.Show(this);

            string curDir = Directory.GetCurrentDirectory();
            this.webBrowser1.Url = new Uri(String.Format("file:///{0}/html/chat.html", curDir));

            //todo: jeśli już connected, ale przychodzi accepted na listenie
            global.thread_HostChecker.threadStart(true, "HostChecker");

            this.listView_hosts.SelectedItems.Clear();

            this.lbl_tbCharsLeft.Text = global.MaxMessageTextLength.ToString();
            this.tb_send.MaxLength = global.MaxMessageTextLength - 1;
            this.tb_send.AllowDrop = true;
            this.tb_send.DragDrop += tb_send_DragDrop;

        }

        void tb_send_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                this.tb_send.Text = (e.Data.GetData(DataFormats.Text)).ToString();
            }
        }


        private bool URL_LOADED = false;

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.URL_LOADED = true;
        }
        private void EXIT_APP()
        {
            if (global.thread_SocketListener.isThreadRunning)
            {
                try
                {
                    global.thread_SocketListener.threadStop();
                    global.thread_SocketListener.listener.Shutdown(SocketShutdown.Both);
                    global.thread_SocketListener.listener.Close();
                }
                catch (Exception ex)
                {
                }
            }

            try
            {
                global.p_Form_log.Close();
            }
            catch (Exception ex)
            {
            }

            //write chat log
            ChatLogFile.writeChatLogToFile(global.ChatLogFileName, global.l_chatLog); //todo: ?

            //save settings
            Properties.Settings.Default.Save();

            System.Windows.Forms.Application.Exit();
            Environment.Exit(0);
        }
        //todo: po zmianie ustawien need to restart i psrawdzić czy się zmieniło
        private void menu_exit_Click(object sender, EventArgs e)
        {
            this.EXIT_APP();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.EXIT_APP();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //this.Hide();
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private bool settingHostList = false;
        private int lastHostSelected = -1;
        public void setHostList()
        {
            this.lastHostSelected = this.listView_hosts.SelectedIndices.Count;
            //global.Log("this.lastHostSelected " + this.lastHostSelected.ToString());
            this.settingHostList = true;
            this.listView_hosts.BeginUpdate();

            this.listView_hosts.DataSource = new List<Network.HOST>();
            this.listView_hosts.DataSource = global.l_hosts;

            foreach (var item in this.listView_hosts.Items)
            {
                item.ImageIndex = 0;
            }

            foreach (var column in this.listView_hosts.Columns)
            {
                column.AllowResize = false;
            }

            this.listView_hosts.Columns[1].AutoResize(BetterListViewColumnHeaderAutoResizeStyle.ColumnContent);
            // this.listView_hosts.AutoResizeColumns(BetterListViewColumnHeaderAutoResizeStyle.HeaderSize);
            // this.listView_hosts.AutoResizeColumns(BetterListViewColumnHeaderAutoResizeStyle.ColumnContent);

            this.settingHostList = false;
            this.listView_hosts.EndUpdate();
        }
        private bool dataSourceChanged = false;
        private void listView_hosts_DataSourceChanged(object sender, EventArgs e)
        {
            this.dataSourceChanged = true;
            //if (this.lastSelectedHostsAmount == 0)
            //{
            //    this.listView_hosts.SelectedIndices.Clear();
            //    //this.lastSelectedHostsAmount = -1;
            //}
        }

        private void showLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global.p_Form_log.Show(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            global.thread_SocketEstablisher.threadStart(true, "SocketEstablisher");
            global.thread_SocketStatusChecker.threadStart(true, "SocketStatusChecker");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            global.thread_SocketListener.threadStart(true, "SocketListener");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Marox.Alert.Warning(global.LocalIPAddress().ToString());

        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.tb_send.Text))
            {
                this.tb_send.Clear();
                return;
            }
            if (!(this.listView_hosts.SelectedIndices.Count > 0))
            {
                this.tb_send.Clear();
                return;
            }

            var chatMessage = new ChatMessage(this.tb_send.Text, Environment.MachineName, chatGetSelectedHost(), DateTime.Now);
            //byte[] msg = Encoding.ASCII.GetBytes(this.tb_send.Text);
            //int bytesSent = (this.listView_hosts.SelectedValue as Network.HOST).socket.Send(msg);
            int bytesSent = (this.listView_hosts.SelectedValue as Network.HOST).socket.Send(chatMessage.Serialize());

            //todo: send - czy jest wybrany host
            //todo: send - czy host jest online!!
            //add to chat
            this.chatAddMessage_me(this.tb_send.Text, DateTime.Now);
            global.l_chatLog.Add(chatMessage);


            //global.Log("SEND: to: " + (this.listView_hosts.SelectedValue as Network.HOST).name + " amount: " + bytesSent);
            this.tb_send_KeyUp(sender, (new KeyEventArgs(Keys.Enter)));
            this.tb_send.Clear();
        }

        private void ChatLog_Add(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var addedItem = e.NewItems[0] as ChatMessage;

            if (addedItem.Sender == this.chatGetSelectedHost())
            {
                this.SafeInvoke(d => d.chatAddMessage_another(addedItem.Message, addedItem.Sender, addedItem.DateSent));
                // this.chatAddMessage_another(addedItem.Message, addedItem.Sender, addedItem.DateSent);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            global.thread_Receiver.threadStart(true, "Receiver");
            global.thread_MessageParser.threadStart(true, "MessageParser");
        }
        //todo: zaznaczony host cały czas
        //todo: czy szukanie host jest nie blokujące!
        private void chatClear()
        {
            this.webBrowser1.Document.InvokeScript("clearMessages");
        }
        private string chatGetSelectedHost()
        {
            if (this.listView_hosts.SelectedIndices.Count > 0)
            {
                return this.listView_hosts.SelectedValue.ToString();
            } 
            else
            {
                return null;
            }
        }
        private void chatSetHeader()
        {
            this.webBrowser1.Document.GetElementById("chat_header").InnerHtml = chatGetSelectedHost();
        }
        private void chatScrollToBottom()
        {
            this.webBrowser1.Document.Body.ScrollIntoView(false);
        }
        private void chatAddMessage_me(string msg, DateTime date)
        {
            this.webBrowser1.Document.InvokeScript("addMessage_me", new string[] { msg, Environment.MachineName, date.ToString() });
            this.chatScrollToBottom();
        }
        private void chatAddMessage_another(string msg, string author, DateTime date)
        {
            this.webBrowser1.Document.InvokeScript("addMessage_another", new string[] { msg, author, date.ToString() });
            this.chatScrollToBottom();
        }
        //todo: sprawdzić metody z serialization.cs
        //todo: wyczyść log czatu
        //todo: opcje - 
        //todo: new message indicator?
        private bool changedOnLaunch = false;
        private void listView_hosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //global.Log("1");
            if (this.lastHostSelected == 0)
            {
                this.lastHostSelected = -1;
                this.changedOnLaunch = true;
                this.listView_hosts.SelectedIndices.Clear();
                return;
            }
            //global.Log("2");
            if (this.settingHostList)
            {
                return;
            }
            //global.Log("3");

            if (this.listView_hosts.SelectedItems.Count > 0)
            {
                this.lbl_selected.Text = this.listView_hosts.SelectedItems[0].Index.ToString();
            }
            else
            {
                this.tb_send.Visible = false;
                this.btn_send.Visible = false;
                this.lbl_tbCharsLeft.Visible = false;
                this.webBrowser1.Visible = false;
                this.lbl_selected.Text = "none";
                return;
            }

            this.tb_send.Visible = true;
            this.btn_send.Visible = true;
            this.lbl_tbCharsLeft.Visible = true;
            this.webBrowser1.Visible = true;
            //set chat header
            this.chatSetHeader();
            //clear chat
            this.chatClear();
            //load messages
            foreach (var msg in global.l_chatLog)
            {
                if (msg.Receiver == chatGetSelectedHost())
                {
                    this.chatAddMessage_me(msg.Message, msg.DateSent);
                }
                if (msg.Sender == chatGetSelectedHost())
                {
                    this.chatAddMessage_another(msg.Message, msg.Sender, msg.DateSent);
                }
            }
        }
        //todo: focus na pole send
        //todo: entery w wiadomości nie wyświetlaja sie
        //todo: nowa linijka po wysłaniu enterem
        private bool sendbox_last_pressed_shift = false;
        private void tb_send_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                this.sendbox_last_pressed_shift = true;
            }
            if (e.KeyCode != Keys.Enter)
            {
                this.sendbox_last_pressed_shift = false;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Shift && e.KeyCode == Keys.Enter)
                {
                    return;
                }

                btn_send_Click(this, new EventArgs());
                this.tb_send.Clear();
            }

        }

        private void tb_send_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            //{
            //    this.sendbox_last_pressed_shift = true;
            //}
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Shift && e.KeyCode == Keys.Enter)
                {
                    return;
                }
                if (this.sendbox_last_pressed_shift)
                {
                    return;
                }

                this.tb_send.Clear();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global.p_Form_options.ShowDialog(this);
        }

        private void tb_send_TextChanged(object sender, EventArgs e)
        {
            this.lbl_tbCharsLeft.Text = (global.MaxMessageTextLength - this.tb_send.Text.Length).ToString();
        }














    }
}

//todo: tb_send dla każdego hosta
//todo: correct local ip
//todo: ordered unordered equality lists