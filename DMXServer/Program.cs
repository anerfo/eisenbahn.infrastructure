using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DMXServer
{
    class Program
    {
        private static DMXCommunication _dmxComm;
        private static TcpLib.TcpServer _server;

        static void Main(string[] args)
        {
             SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

             _dmxComm = new DMXCommunication();
            _dmxComm.StartCommunication();
            _dmxComm.SetNumberOfChannels(12);

            DMXServiceProvider dmxServiceProvider = new DMXServiceProvider(_dmxComm);

            _server = new TcpLib.TcpServer(dmxServiceProvider, 12409);
            _server.Start();

            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            try
            {
                _server.Stop();
                while (_server.CurrentConnections > 0)
                {
                    System.Threading.Thread.Sleep(100);
                }
                _dmxComm.StopCommunication();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }

        #region unmanaged

        // Declare the SetConsoleCtrlHandler function
        // as external and receiving a delegate. 
        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

 

        // A delegate type to be used as the handler routine 
        // for SetConsoleCtrlHandler.
        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

 

        // An enumerated type for the control messages
        // sent to the handler routine.
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }
        #endregion
    }
}
