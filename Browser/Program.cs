using CommandLine;
using System;
using System.Windows.Forms;

namespace Browser
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                Application.Run(new BrowserWindow(o.Uri, o.Title, 
                    o.EventAddress == null ? null : new BrowserEventServer(o.EventAddress)));
            });
        }
    }
}
