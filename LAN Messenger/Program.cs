using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAN_Messenger
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (object sender, UnhandledExceptionEventArgs e) => { Marox.Alert.Error("UNHANDLED EXCEPTION! \n\n" + e.ExceptionObject.ToString()); };

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
                mutex.ReleaseMutex();
            }
            else
            {
                Marox.Alert.Error("Another instance of " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " is already running");
            }
        }
    }
}
