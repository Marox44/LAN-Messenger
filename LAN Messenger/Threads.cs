using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marox.ExtensionMethods;
using System.Net.Sockets;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace LAN_Messenger
{
    namespace Threads
    {
        public class HostChecker : Marox.THREAD
        {
            protected sealed override void run()
            {
                global.Log("Threading: HostChecker thread started. ID: " + AppDomain.GetCurrentThreadId().ToString());
                while (this.isThreadRunning)
                {
                    //todo: mutexy ...?!
                    //global.mutex_hosts.WaitOne();
                    var list_current = new List<Network.HOST>(global.l_hosts);
                    //global.mutex_hosts.ReleaseMutex();

                    var list_new = Network.LAN.getLanHosts();

                    //bool areEqual = list_current.SequenceEqual(list_new);
                    bool areEqual = new HashSet<Network.HOST>(list_current).SetEquals(list_new);
                    // bool areEqual = global.ScrambledEquals(list_current, list_new);
                    //bool areEqual = (list_current.Count == list_new.Count) && (firstNotSecond.Count == secondNotFirst.Count);
                    if (!areEqual)
                    {
                        foreach (var host in list_current.ToList())
                        {
                            if (!list_new.Contains(host))
                            {
                                list_current.Remove(host);
                            }
                        }
                        foreach (var host in list_new)
                        {
                            if (!list_current.Contains(host))
                            {
                                list_current.Add(host);
                            }
                        }

                        Mutex.WaitAll(global.mutex_hosts);
                        global.l_hosts = list_current;
                        // global.mutex_hosts.ReleaseMutex();
                        foreach (var m in global.mutex_hosts)
                        {
                            m.ReleaseMutex();
                        }

                        global.p_main.SafeInvoke(d => d.setHostList());
                        global.Log("HostChecker: Host list updated");
                    }

                    //global.mutex_hosts.ReleaseMutex();
                    //todo: release wszystkie

                    //todo: co kiedy lista się zmieni -> event zmiana indeksu??
                    global.Log("HostChecker: Searching hosts... Hosts found: " + list_new.Count);
                    //temp
                    {
                        //var _h = new Network.HOST();
                        //_h.ip = IPAddress.Parse("127.0.0.1");
                        //_h.name = "DELL-WIN7";
                        //global.mutex_hosts.WaitOne();
                        //global.l_hosts.Add(_h);
                        //global.mutex_hosts.ReleaseMutex();
                        //global.p_main.SafeInvoke(d => d.setHostList());
                    }
                    //temp

                    System.Threading.Thread.Sleep(60000);
                }
            }
        }
        //todo: threads timers?
        public class SocketStatusChecker : Marox.THREAD
        {
            protected sealed override void run()
            {
                //todo: reconnecting?
                global.Log("Threading: SocketStatusChecker thread started. ID: " + AppDomain.GetCurrentThreadId().ToString());
                while (this.isThreadRunning)
                {
                    global.mutex_hosts[2].WaitOne();

                    foreach (var host in global.l_hosts)
                    {
                        if (host.socket != null)
                        {
                            bool status = Network.Tools.IsSocketConnected(host.socket);
                            if (status)
                            {
                                host.status = Network.HostStatus.connected;
                            }
                            else
                            {
                                host.mutex.WaitOne();

                                host.socket.Shutdown(SocketShutdown.Both);
                                host.socket.Close();
                                host.socket = null;

                                host.mutex.ReleaseMutex();

                                host.status = Network.HostStatus.offline;
                            }
                        }
                        else
                        {
                            host.status = Network.HostStatus.offline;
                        }
                    }

                    global.mutex_hosts[2].ReleaseMutex();

                    global.p_main.SafeInvoke(d => d.setHostList());

                    global.Log("SocketStatusChecker: Sockets status refreshed");
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

        public class SocketEstablisher : Marox.THREAD
        {
            protected sealed override void run()
            {
                global.Log("Threading: SocketEstablisher thread started. ID: " + AppDomain.GetCurrentThreadId().ToString());
                while (this.isThreadRunning)
                {
                    global.mutex_hosts[0].WaitOne();

                    foreach (var host in global.l_hosts)
                    {
                        host.mutex.WaitOne();
                        if (host.socket == null)
                        {
                            host.mutex.ReleaseMutex();

                            IPEndPoint target = new IPEndPoint(host.ip, global.PORT);
                            Socket s = this.tryToConnect(target);

                            if (s != null)
                            {
                                global.Log("SocketEstablisher: Connecting to " + target.Address + ":" + target.Port + "  successful");

                                // s.ReceiveTimeout = 200;
                                host.mutex.WaitOne();

                                if (host.socket == null)
                                {
                                    host.socket = s;
                                }
                                else
                                {
                                    host.socket.Shutdown(SocketShutdown.Both);
                                    host.socket.Close();
                                    host.socket = s;
                                }

                                host.mutex.ReleaseMutex();
                            }
                            else
                            {
                                //host.socket = null;
                            }
                        }
                        else
                        {
                            host.mutex.ReleaseMutex();
                        }
                    }

                    global.mutex_hosts[0].ReleaseMutex();

                    System.Threading.Thread.Sleep(100);
                }
            }//todo: AERO LAYOUT!!!

            private Socket tryToConnect(IPEndPoint Target)
            {
                Socket socket = null;
                //todo: connect timeout??
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        socket.Connect(Target);
                    }
                    catch (Exception ex)
                    {
                        global.Log("SocketEstablisher: Connecting to " + Target.Address + ":" + Target.Port + " failed: " + ex.Message);
                        socket = null;
                    }
                }
                catch (Exception ex)
                {
                    global.Log("SocketEstablisher: Creating socket failed: " + ex.Message);
                    socket = null;
                }

                return socket;
            }
        }

        //todo: proper exit
        public class SocketListener : Marox.THREAD
        {
            internal Socket listener;
            protected sealed override void run()
            {
                global.Log("Threading: SocketListener thread started. ID: " + AppDomain.GetCurrentThreadId().ToString());

                while (this.isThreadRunning)
                {
                    //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                    //IPAddress ipAddress = global.LocalIPAddress();
                    IPAddress ipAddress = IPAddress.Parse(Properties.Settings.Default.LocalIP);
                    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, global.PORT);
                    this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        listener.Bind(localEndPoint);
                        listener.Listen(20);
                    }
                    catch (Exception ex)
                    {
                        global.Log("SocketListener: Error creating listener socket: " + ex.Message);
                    }

                    while (this.isThreadRunning)
                    {
                        global.Log("SocketListener: Listening for connections on listener socket. Endpoint: " + localEndPoint.Address + ":" + localEndPoint.Port);
                        try
                        {
                            Socket handler = listener.Accept();

                            IPEndPoint remoteEndPoint = handler.RemoteEndPoint as IPEndPoint;
                            global.Log("SocketListener: Incoming connection from " + remoteEndPoint.Address.ToString() + " accepted");

                            //test
                            global.mutex_hosts[1].WaitOne();
                            foreach (var host in global.l_hosts)
                            {
                                if (host.ip.Equals(remoteEndPoint.Address))
                                {
                                    host.mutex.WaitOne();
                                    if (host.socket == null)
                                    {
                                        host.socket = handler;
                                    }
                                    else
                                    {
                                        host.socket.Shutdown(SocketShutdown.Both);
                                        host.socket.Close();
                                        host.socket = handler;
                                    }
                                    host.mutex.ReleaseMutex();
                                    break;
                                }
                            }
                            global.mutex_hosts[1].ReleaseMutex();
                            //test
                        }
                        catch (Exception ex)
                        {
                            global.Log("SocketListener: Error on listener.Accept() : " + ex.Message);
                        }
                    }
                }
            }
        }
        //todo: auto wykrywanie linków?
        //todo: dzielenie wiadomości przy receive
        //todo: czy ikonka znika po zamknięciu ?
        //todo: drag drop
        public sealed class Receiver : Marox.THREAD
        {
            protected sealed override void run()
            {
                global.Log("Threading: Receiver thread started. ID: " + AppDomain.GetCurrentThreadId().ToString());
                while (this.isThreadRunning)
                {
                    foreach (var host in global.l_hosts)
                    {
                        host.mutex.WaitOne();
                        if (host.socket != null) //todo: here
                        {
                            if (Network.Tools.IsSocketConnected(host.socket))
                            {
                                try
                                {
                                    string data = null;
                                    byte[] bytes = new byte[global.ReceiveBufferSize];
                                    host.socket.ReceiveTimeout = 1;
                                    int bytesRec = host.socket.Receive(bytes);

                                    global.thread_MessageParser.mutex_buffer.WaitOne();
                                    global.thread_MessageParser.buffer = bytes;
                                    global.thread_MessageParser.mutex_buffer.ReleaseMutex();

                                    // Marox.Alert.Info(System.Text.Encoding.ASCII.GetString(bytes, 220, 5));

                                    //data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                    //if (data.IndexOf("<EOF>") > -1)
                                    //{
                                    //    break;
                                    //}

                                    global.Log("Received: " + bytesRec + " bytes \t on: " + host.name + "(" + host.ip.ToString() + ")");
                                }
                                catch (Exception ex)
                                {
                                    int code = -1;
                                    var w32ex = ex as Win32Exception;
                                    if (w32ex == null)
                                    {
                                        w32ex = ex.InnerException as Win32Exception;
                                    }
                                    if (w32ex != null)
                                    {
                                        code = w32ex.ErrorCode;
                                    }
                                    if (code != 10060)
                                    {
                                        global.Log("Receiver: " + ex.Message + " code: " + code);
                                    }
                                }
                            }
                        }
                        host.mutex.ReleaseMutex();
                    }
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        public sealed class MessageParser : Marox.THREAD
        {
            public Mutex mutex_buffer = new Mutex();
            public byte[] buffer = null;

            protected sealed override void run()
            {
                global.Log("Threading: MessageParser thread started. ID: " + AppDomain.GetCurrentThreadId().ToString());
                while (this.isThreadRunning)
                {
                    byte[] buf = null;
                    this.mutex_buffer.WaitOne();
                    if (this.buffer != null)
                    {
                        buf = this.buffer;
                        this.buffer = null;
                    }
                    this.mutex_buffer.ReleaseMutex();

                    if (buf != null)
                    {
                        ChatMessage msg = ChatMessage.Deserialize(buf);
                        if (msg != null)
                        {
                            global.l_chatLog.Add(msg);
                        }
                    }

                    //System.Threading.Thread.Sleep(1);
                    //todo: queue??????????
                }
            }
        }
    }
}

//todo: global.chatlog synchronizacja???
//todo: pause log window
//todo: tooltip on host list
//todo: on minimize hide log