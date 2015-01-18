using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Composition;
using System.Windows;

namespace WindowsNotifierForWin7
{
    [Export(typeof(INotifier))]
    [ExportMetadata("Title", "WindowsNotifier For Windows7")]
    [ExportMetadata("Description", "在win7下模仿win8的通知效果")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("Author", "@AMing")]
    public class WindowsNotifier : INotifier
    {
        private ToastWindow ToastWindow;

        public WindowsNotifier()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {
        }
        private void InitToastWindow()
        {
            if (this.ToastWindow != null) return;

            this.ToastWindow = new ToastWindow();
            Application.Current.MainWindow.Closing += (sender, e) => this.ToastWindow.Close();
        }

        public void Show(NotifyType type, string header, string body, Action activated, Action<Exception> failed = null)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    InitToastWindow();
                    this.ToastWindow.Show();
                    this.ToastWindow.WindowState = WindowState.Normal;
                    this.ToastWindow.ShowToast(header, body);
                }
                catch (Exception ex)
                {
                    if (failed != null)
                        failed(ex);
                }
            }));
        }

        public object GetSettingsView()
        {
            return null;
        }
    }
}