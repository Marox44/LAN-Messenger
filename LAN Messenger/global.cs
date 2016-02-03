using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marox.ExtensionMethods;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections.ObjectModel;

namespace LAN_Messenger
{
    public static class global
    {
        static global()
        {
            //Init Properties.Settings <-
            //Marox.Alert.Info(Properties.Settings.Default.LocalIP);
            var ips = Network.Tools.getLocalIPAddresses();
            IPAddress tmpIP;
            if (!(IPAddress.TryParse(Properties.Settings.Default.LocalIP, out tmpIP)))
            {
                Properties.Settings.Default.LocalIP = Network.Tools.getLocalIPAddresses()[0].ToString();
            }
            else
            {
                if (!(ips.Exists(d => d.Equals(IPAddress.Parse(Properties.Settings.Default.LocalIP)))))
                {
                    Properties.Settings.Default.LocalIP = Network.Tools.getLocalIPAddresses()[0].ToString();
                }
                //Marox.Alert.Info(Properties.Settings.Default.LocalIP);
            }

            //init mutex array
            for (int i = 0; i < mutex_hosts.Length; i++)
            {
                mutex_hosts[i] = new Mutex();
            }

        }

        //Config
        public static int PORT = 11000;
        public static string ChatLogFileName = "chatlog.bin";
        public static int ReceiveBufferSize = 16384;
        public static int MaxMessageTextLength = 15000;

        //Forms
        public static MainWindow p_main = null;
        public static Form_log p_Form_log = Marox.Singleton<Form_log>.Instance;
        public static Form_options p_Form_options = Marox.Singleton<Form_options>.Instance;

        //Global variables
        public static List<Network.HOST> l_hosts = new List<Network.HOST>();
        public static Mutex[] mutex_hosts = new Mutex[3];

        public static ObservableCollection<ChatMessage> l_chatLog = new ObservableCollection<ChatMessage>();
        public static Mutex mutex_chatLog = new Mutex();

        private static bool _hostListLoaded = false;
        public static bool IsHostListLoaded
        {
            set
            {
                if (_hostListLoaded == false && value == true)
                {
                    global.p_main.panel_hostListLoading.SafeInvoke(d => d.Visible = false);
                    global.p_main.panel_hostListLoading.SafeInvoke(d => d.Hide());
                }
                _hostListLoaded = value;
            }
        }

        public static List<IPAddress> localIPAddresses;


        //Threads
        public static Threads.HostChecker thread_HostChecker = Marox.Singleton<Threads.HostChecker>.Instance;
        public static Threads.SocketEstablisher thread_SocketEstablisher = Marox.Singleton<Threads.SocketEstablisher>.Instance;
        public static Threads.SocketStatusChecker thread_SocketStatusChecker = Marox.Singleton<Threads.SocketStatusChecker>.Instance;
        public static Threads.SocketListener thread_SocketListener = Marox.Singleton<Threads.SocketListener>.Instance;
        public static Threads.Receiver thread_Receiver = Marox.Singleton<Threads.Receiver>.Instance;
        public static Threads.MessageParser thread_MessageParser = Marox.Singleton<Threads.MessageParser>.Instance;








        //Methods
        public static void Log(string Text)
        {
            global.p_Form_log.SafeInvoke(d => d.addLog(Text));
        }


        public static bool UnorderedEqual<T>(ICollection<T> a, ICollection<T> b)
        {
            // 1
            // Require that the counts are equal
            if (a.Count != b.Count)
            {
                return false;
            }
            // 2
            // Initialize new Dictionary of the type
            Dictionary<T, int> d = new Dictionary<T, int>();
            // 3
            // Add each key's frequency from collection A to the Dictionary
            foreach (T item in a)
            {
                int c;
                if (d.TryGetValue(item, out c))
                {
                    d[item] = c + 1;
                }
                else
                {
                    d.Add(item, 1);
                }
            }
            // 4
            // Add each key's frequency from collection B to the Dictionary
            // Return early if we detect a mismatch
            foreach (T item in b)
            {
                int c;
                if (d.TryGetValue(item, out c))
                {
                    if (c == 0)
                    {
                        return false;
                    }
                    else
                    {
                        d[item] = c - 1;
                    }
                }
                else
                {
                    // Not in dictionary
                    return false;
                }
            }
            // 5
            // Verify that all frequencies are zero
            foreach (int v in d.Values)
            {
                if (v != 0)
                {
                    return false;
                }
            }
            // 6
            // We know the collections are equal
            return true;
        }

        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }
    }
}
