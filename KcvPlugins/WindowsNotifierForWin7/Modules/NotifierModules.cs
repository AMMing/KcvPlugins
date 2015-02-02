using AMing.Plugins.Core.Modules;
using System.Collections.Generic;
using System.Linq;
using AMing.Plugins.Core.Extensions;
using System.Windows.Media;
using System.Windows;
using System;
using AMing.Plugins.Core.Models;

namespace AMing.WindowsNotifierForWin7.Modules
{
    public class NotifierModules : ModulesBase
    {
        #region Current

        private static NotifierModules _current = new NotifierModules();

        public static NotifierModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            InitPublicModules();
        }

        #region method


        private ToastWindow ToastWindow;
        private void InitToastWindow()
        {
            if (this.ToastWindow != null) return;

            this.ToastWindow = new ToastWindow();
            Application.Current.MainWindow.Closing += (sender, e) => this.ToastWindow.Close();
        }

        public void Notify(Enums.ToastType type, string header, string body, Action activated = null, Action<Exception> failed = null)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    InitToastWindow();
                    this.ToastWindow.Show();
                    this.ToastWindow.WindowState = WindowState.Normal;
                    this.ToastWindow.ShowToast(type, header, body);
                    this.ToastWindow.ToastClickAction = activated;
                }
                catch (Exception ex)
                {
                    if (failed != null)
                        failed(ex);
                }
            }));
        }

        public void Notify(string header, string body, Action activated = null, Action<Exception> failed = null)
        {
            this.Notify(Enums.ToastType.Notification, header, body, activated, failed);
        }

        public void Warning(string header, string body)
        {
            this.Notify(Enums.ToastType.Warning, header, body);
        }

        #endregion

        #region PublicModules

        private void InitPublicModules()
        {
            AMing.Plugins.Core.GenericMessager.Current.RegisterForMessage(this, Plugins.Core.Enums.MessageType.Notification, msg => Notify(msg.Title, msg.Content));
            AMing.Plugins.Core.GenericMessager.Current.RegisterForMessage(this, Plugins.Core.Enums.MessageType.Warning, msg => Warning(msg.Title, msg.Content));
        }

        #endregion


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
