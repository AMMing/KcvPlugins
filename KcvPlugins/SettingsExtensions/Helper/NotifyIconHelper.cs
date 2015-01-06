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

        private static NotifyIconHelper _current = new NotifyIconHelper();

        public static NotifyIconHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        winforms.NotifyIcon _notifyIcon;
        Window mainWindow;
        WindowState oldwinState = WindowState.Normal;

        public void Init()
        {
            mainWindow = Application.Current.MainWindow;

            InitNotifyIcon();
            Enable(Data.Settings.Current.EnableNotifyIcon);

            BindEvent();
        }

        #region method

        void InitNotifyIcon()
        {
            try
            {
                var iconPath = ToolSettings.Default.NotifyIcon_Path;
                if (mainWindow.Icon != null)
                {
                    iconPath = mainWindow.Icon.ToString();
                }

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
                    _notifyIcon.DoubleClick += _notifyIcon_DoubleClick;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void BindEvent()
        {
            mainWindow.StateChanged += MainWindow_StateChanged;
            mainWindow.Closed += MainWindow_Closed;
        }

        public void Enable(bool isEnable)
        {
            Data.Settings.Current.EnableNotifyIcon = isEnable;
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = Data.Settings.Current.EnableNotifyIcon;
            }
        }


        void ShowHideWindow()
        {
            if (mainWindow.WindowState == WindowState.Minimized)
            {
                mainWindow.Show();
                mainWindow.WindowState = oldwinState;
            }
            else
            {
                mainWindow.WindowState = WindowState.Minimized;
            }
        }

        #endregion

        #region event

        void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (mainWindow.WindowState == WindowState.Minimized)
            {
                mainWindow.Hide();
            }
            else
            {
                oldwinState = mainWindow.WindowState;
            }
        }
        void MainWindow_Closed(object sender, EventArgs e)
        {
            mainWindow.StateChanged -= MainWindow_StateChanged;
            mainWindow.Closed -= MainWindow_Closed;
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }

        void showhideItem_Click(object sender, EventArgs e)
        {
            ShowHideWindow();
        }
        void exitItem_Click(object sender, EventArgs e)
        {
            mainWindow.Close();
        }

        void _notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowHideWindow();
        }
        #endregion
    }
}
