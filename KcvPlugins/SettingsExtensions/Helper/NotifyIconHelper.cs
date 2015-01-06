using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using kcv = Grabacr07.KanColleViewer;
using winforms = System.Windows.Forms;

namespace AMing.SettingsExtensions.Helper
{
    public class NotifyIconHelper
    {

        #region Current

        private static ExitTipHelper _current = new ExitTipHelper();

        public static ExitTipHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        static bool isInit = false;

        public void Init()
        {
            if (isInit)
            {
                return;
            }

            kcvMainWindow = kcv.App.Current.MainWindow;

            InitNotifyIcon();
            Enable(Data.Settings.Current.Enable_NotifyIcon);

            BindEvent();
        }

        winforms.NotifyIcon _notifyIcon;
        Window kcvMainWindow;


        void InitNotifyIcon()
        {
            try
            {
                var iconPath = kcvMainWindow.Icon.ToString()
                                ?? ToolSettings.Default.NotifyIcon_Path;

                Uri iconUri = new Uri(iconPath, UriKind.Absolute);
                using (var icon_stream = Application.GetResourceStream(iconUri).Stream)
                {
                    winforms.ContextMenu contextMenu = new winforms.ContextMenu();

                    winforms.MenuItem showhideItem = new winforms.MenuItem
                    {
                        Text = TextResource.NotifyIcon_ContextMenu_ShowHide
                    };
                    winforms.MenuItem exitItem = new winforms.MenuItem
                    {
                        Text = TextResource.NotifyIcon_ContextMenu_Exit
                    };
                    showhideItem.Click += showhideItem_Click;
                    exitItem.Click += exitItem_Click;

                    contextMenu.MenuItems.Add(showhideItem);
                    contextMenu.MenuItems.Add(exitItem);

                    _notifyIcon = new System.Windows.Forms.NotifyIcon
                    {
                        Text = TextResource.NotifyIcon_Text,
                        Icon = new Icon(icon_stream),
                        ContextMenu = contextMenu
                    };
                }
            }
            catch (Exception)
            {
            }
        }


        public void Enable(bool isEnable)
        {
            Data.Settings.Current.Enable_NotifyIcon = isEnable;
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = Data.Settings.Current.Enable_NotifyIcon;
            }
        }

        void BindEvent()
        {
            kcvMainWindow.StateChanged += MainWindow_StateChanged;
            kcvMainWindow.Closed += kcvMainWindow_Closed;
        }

        void kcvMainWindow_Closed(object sender, EventArgs e)
        {
            kcvMainWindow.StateChanged -= MainWindow_StateChanged;
            kcvMainWindow.Closed -= kcvMainWindow_Closed;
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }

        WindowState oldwinState = WindowState.Normal;
        void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (kcvMainWindow.WindowState != WindowState.Minimized)
            {
                kcvMainWindow.Hide();
            }
            else
            {
                oldwinState = kcvMainWindow.WindowState;
            }
        }

        #region event

        void showhideItem_Click(object sender, EventArgs e)
        {
            if (kcvMainWindow.WindowState == WindowState.Minimized)
            {
                kcvMainWindow.Show();
                kcvMainWindow.WindowState = oldwinState;
            }
            else
            {
                kcvMainWindow.WindowState = WindowState.Minimized;
            }
        }
        void exitItem_Click(object sender, EventArgs e)
        {
            kcvMainWindow.Close();
        }

        #endregion


    }
}
