using CefSharp;
using CefSharp.WinForms;
using System.Diagnostics;
using System.Windows.Forms;

namespace Browser
{
    public partial class BrowserWindow : Form
    {
        public ChromiumWebBrowser _ChromeBrowser;

        public BrowserWindow(string uri, string title, IBrowserEventHandler browserEventHandler)
        {
            InitializeComponent();
            InitializeChromium(uri, browserEventHandler);
            Text = title;
            ShowIcon = false;
        }

        public void InitializeChromium(string uri, IBrowserEventHandler browserEventHandler)
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            _ChromeBrowser = new ChromiumWebBrowser(uri);
            this.Controls.Add(_ChromeBrowser);
            _ChromeBrowser.Dock = DockStyle.Fill;
            _ChromeBrowser.AddressChanged += (sender, args) => browserEventHandler?.AddressChanged(args.Address);
        }

        private void BrowserWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
