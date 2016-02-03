using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;

namespace LAN_Messenger
{
    namespace Network
    {
        public enum HostStatus
        {
            offline,
            connecting,
            connected,
            disconnected
        }
        public class HOST : System.Object
        {
            public HOST()
            {
                this.status = HostStatus.offline;
                this.mutex = new Mutex();
            }

            public override string ToString()
            {
                return this.name;
            }
            public override bool Equals(System.Object obj)
            {
                // If parameter is null return false.
                if (obj == null)
                {
                    return false;
                }

                // If parameter cannot be cast to Point return false.
                HOST p = obj as HOST;
                if ((System.Object)p == null)
                {
                    return false;
                }

                // Return true if the fields match:
                return (this.name == p.name) && (this.ip.Equals(p.ip));
            }
            public bool Equals(HOST p)
            {
                // If parameter is null return false:
                if ((object)p == null)
                {
                    return false;
                }

                // Return true if the fields match:
                return (this.name == p.name) && (this.ip.Equals(p.ip));
            }
            public override int GetHashCode()
            {
                return this.name.GetHashCode() ^ this.ip.GetHashCode();
                //return this.name.GetHashCode();
            }
            public static bool operator ==(HOST a, HOST b)
            {
                // If both are null, or both are same instance, return true.
                if (System.Object.ReferenceEquals(a, b))
                {
                    return true;
                }

                // If one is null, but not both, return false.
                if (((object)a == null) || ((object)b == null))
                {
                    return false;
                }

                // Return true if the fields match:
                return a.name == b.name && a.ip.Equals(b.ip);
            }
            public static bool operator !=(HOST a, HOST b)
            {
                return !(a == b);
            }



            [DisplayName("Host")]
            public string name { get; set; }
            [DisplayName("Status")]
            public HostStatus status { get; set; }

            internal IPAddress ip { get; set; }
            internal Socket socket { get; set; }
            internal Mutex mutex { get; set; }
        }

        #region NetworkBrowser CLASS
        /// <summary>
        /// Provides a mechanism for supplying
        // a list of all PC names in the local network.
        /// This collection of PC names is used in the form 
        /// 


        /// This class makes use of a DllImport instruction.
        /// The purpose of which is as follows:
        /// When a DllImport declaration is made
        /// in managed code (C#) it is a call to a legacy
        /// unmanaged code module, normally
        /// a C++ Dynamic Link Library. These C++ Dll's are
        /// usually part of the operating system API,
        /// or some other vendors API, and must be 
        /// used to carry out operations that are not
        /// native within the managed code C# framework. 
        /// This is fairly normal within the windows world.
        /// The only thing that needs careful consideration
        /// is the construction of the correct type of STRUCTS,
        /// object pointers, and attribute markers,
        /// which all contribute to making the link
        /// between managed (C#) and unmanaged code (C++)
        /// more seamless
        /// 

        /// This class makes use of the following Dll calls
        /// <list type="bullet">
        /// <item>
        /// <description> Netapi32.dll : NetServerEnum,
        /// The NetServerEnum function lists all servers
        /// of the specified type that are visible in
        /// a domain. For example, an application can call 
        /// NetServerEnum to list all domain controllers
        /// only or all SQL servers only.
        /// You can combine bit masks to list several
        /// types. For example, a value of 0x00000003 
        /// combines the bit masks for SV_TYPE_WORKSTATION
        /// (0x00000001) and SV_TYPE_SERVER (0x00000002).
        /// </description>
        /// </item>
        /// <item>
        /// <description> Netapi32.dll : NetApiBufferFree,
        /// The NetApiBufferFree function frees 
        /// the memory that the NetApiBufferAllocate
        /// function allocates. Call NetApiBufferFree 
        /// to free the memory that other network
        /// management functions return.</description>
        /// </item>
        /// </list>
        /// </summary>
        public sealed class NetworkBrowser
        {
            #region Dll Imports

            //declare the Netapi32 : NetServerEnum method import
            [DllImport("Netapi32", CharSet = CharSet.Auto,
            SetLastError = true),
            SuppressUnmanagedCodeSecurityAttribute]

            /// <summary>
            /// Netapi32.dll : The NetServerEnum function lists all servers
            /// of the specified type that are
            /// visible in a domain. For example, an 
            /// application can call NetServerEnum
            /// to list all domain controllers only
            /// or all SQL servers only.
            /// You can combine bit masks to list
            /// several types. For example, a value 
            /// of 0x00000003  combines the bit
            /// masks for SV_TYPE_WORKSTATION 
            /// (0x00000001) and SV_TYPE_SERVER (0x00000002)
            /// </summary>
            public static extern int NetServerEnum(
                string ServerNane, // must be null
                int dwLevel,
                ref IntPtr pBuf,
                int dwPrefMaxLen,
                out int dwEntriesRead,
                out int dwTotalEntries,
                int dwServerType,
                string domain, // null for login domain
                out int dwResumeHandle
                );

            //declare the Netapi32 : NetApiBufferFree method import
            [DllImport("Netapi32", SetLastError = true),
            SuppressUnmanagedCodeSecurityAttribute]

            /// <summary>
            /// Netapi32.dll : The NetApiBufferFree function frees 
            /// the memory that the NetApiBufferAllocate function allocates. 
            /// Call NetApiBufferFree to free
            /// the memory that other network 
            /// management functions return.
            /// </summary>
            public static extern int NetApiBufferFree(
                IntPtr pBuf);

            //create a _SERVER_INFO_100 STRUCTURE
            [StructLayout(LayoutKind.Sequential)]
            public struct _SERVER_INFO_100
            {
                internal int sv100_platform_id;
                [MarshalAs(UnmanagedType.LPWStr)]
                internal string sv100_name;
            }
            #endregion
            #region Public Constructor
            /// <SUMMARY>
            /// Constructor, simply creates a new NetworkBrowser object
            /// </SUMMARY>
            public NetworkBrowser()
            {

            }
            #endregion
            #region Public Methods
            /// <summary>
            /// Uses the DllImport : NetServerEnum
            /// with all its required parameters
            /// (see http://msdn.microsoft.com/library/default.asp?
            ///      url=/library/en-us/netmgmt/netmgmt/netserverenum.asp
            /// for full details or method signature) to
            /// retrieve a list of domain SV_TYPE_WORKSTATION
            /// and SV_TYPE_SERVER PC's
            /// </summary>
            /// <returns>Arraylist that represents
            /// all the SV_TYPE_WORKSTATION and SV_TYPE_SERVER
            /// PC's in the Domain</returns>
            public ArrayList getNetworkComputers()
            {
                //local fields
                ArrayList networkComputers = new ArrayList();
                const int MAX_PREFERRED_LENGTH = -1;
                int SV_TYPE_WORKSTATION = 1;
                int SV_TYPE_SERVER = 2;
                IntPtr buffer = IntPtr.Zero;
                IntPtr tmpBuffer = IntPtr.Zero;
                int entriesRead = 0;
                int totalEntries = 0;
                int resHandle = 0;
                int sizeofINFO = Marshal.SizeOf(typeof(_SERVER_INFO_100));


                try
                {
                    //call the DllImport : NetServerEnum 
                    //with all its required parameters
                    //see http://msdn.microsoft.com/library/
                    //default.asp?url=/library/en-us/netmgmt/netmgmt/netserverenum.asp
                    //for full details of method signature
                    int ret = NetServerEnum(null, 100, ref buffer,
                        MAX_PREFERRED_LENGTH,
                        out entriesRead,
                        out totalEntries, SV_TYPE_WORKSTATION |
                        SV_TYPE_SERVER, null, out 
                    resHandle);
                    //if the returned with a NERR_Success 
                    //(C++ term), =0 for C#
                    if (ret == 0)
                    {
                        //loop through all SV_TYPE_WORKSTATION 
                        //and SV_TYPE_SERVER PC's
                        for (int i = 0; i < totalEntries; i++)
                        {
                            //get pointer to, Pointer to the 
                            //buffer that received the data from
                            //the call to NetServerEnum. 
                            //Must ensure to use correct size of 
                            //STRUCTURE to ensure correct 
                            //location in memory is pointed to
                            tmpBuffer = new IntPtr((int)buffer +
                                       (i * sizeofINFO));
                            //Have now got a pointer to the list 
                            //of SV_TYPE_WORKSTATION and 
                            //SV_TYPE_SERVER PC's, which is unmanaged memory
                            //Needs to Marshal data from an 
                            //unmanaged block of memory to a 
                            //managed object, again using 
                            //STRUCTURE to ensure the correct data
                            //is marshalled 
                            _SERVER_INFO_100 svrInfo = (_SERVER_INFO_100)
                                Marshal.PtrToStructure(tmpBuffer,
                                        typeof(_SERVER_INFO_100));

                            //add the PC names to the ArrayList
                            networkComputers.Add(svrInfo.sv100_name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem with acessing " +
                        "network computers in NetworkBrowser " +
                        "\r\n\r\n\r\n" + ex.Message,
                        "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return null;
                }
                finally
                {
                    //The NetApiBufferFree function frees 
                    //the memory that the 
                    //NetApiBufferAllocate function allocates
                    NetApiBufferFree(buffer);
                }
                //return entries found
                return networkComputers;

            }
            #endregion
        }
        #endregion
        static public class LAN
        {
            public static List<string> getLanHostnames2()
            {
                List<string> l_hosts = new List<string>();
                DirectoryEntry root = new DirectoryEntry("WinNT:");
                foreach (DirectoryEntry computers in root.Children)
                {
                    foreach (DirectoryEntry computer in computers.Children)
                    {
                        if (computer.Name != "Schema")
                        {
                            if (computer.Name != Environment.MachineName)
                            {
                                l_hosts.Add(computer.Name);
                            }
                        }
                    }
                }
                global.IsHostListLoaded = true;
                return l_hosts;
            }
            public static List<string> getLanHostnames()
            {
                var s_comps = new List<string>();

                var nb = new NetworkBrowser();
                var computers = nb.getNetworkComputers();
                for (int i = 0; i < computers.Count; i++)
                {
                    var o = computers[i];
                    if (o.ToString() == Environment.MachineName)
                    {
                        computers.RemoveAt(i);
                    }
                }
                for (int i = 0; i < computers.Count; i++)
                {
                    s_comps.Add(computers[i].ToString());
                }

                return s_comps;
            }
            public static List<IPAddress> getLanHostAddresses(string hostname)
            {//todo: qwe
                IPAddress[] ips;
                var l_ips = new List<IPAddress>();

                try
                {
                    ips = Dns.GetHostAddresses(hostname);
                    foreach (var ip in ips)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                            l_ips.Add(ip);
                    }
                }
                catch (Exception)
                {

                }


                return l_ips;
            }
            public static List<HOST> getLanHosts()
            {
                var l_hosts = new List<HOST>();
                var hostnames = getLanHostnames2();  // <------------ temp
                foreach (var host in hostnames)
                {
                    HOST h = new HOST();
                    h.name = host;
                    var t_ip = getLanHostAddresses(host);
                    if (t_ip.Count > 0)
                    {
                        h.ip = t_ip[0];
                        l_hosts.Add(h);
                    }

                }

                return l_hosts;
            }
        }

        public static class Tools
        {
            public static bool IsSocketConnected(Socket s)
            {
                try
                {
                    bool part1 = s.Poll(1000, SelectMode.SelectRead);
                    bool part2 = (s.Available == 0);
                    if (part1 && part2)
                        return false;
                    else
                        return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            public static IPAddress getLocalIPAddress()
            {
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    return null;
                }

                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            }
            public static List<IPAddress> getLocalIPAddresses()
            {
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    return null;
                }

                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                //todo: listener na dobrym ip
                var list = new List<IPAddress>();

                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        list.Add(ip);
                    }
                }

                return list;
            }
        }


    }
}
