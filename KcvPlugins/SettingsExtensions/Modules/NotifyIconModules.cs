using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using kcv = Grabacr07.KanColleViewer;
using winforms = System.Windows.Forms;

namespace AMing.SettingsExtensions.Modules
{
    public class NotifyIconModules : ModulesBase
    {
        #region Current

        private static NotifyIconModules _current = new NotifyIconModules();

        public static NotifyIconModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        winforms.NotifyIcon _notifyIcon;
        Window mainWindow;
        WindowState oldwinState = WindowState.Normal;

        public override void Initialize()
        {
            base.Initialize();
            mainWindow = Application.Current.MainWindow;

            InitNotifyIcon();
            Enable(Data.Settings.Current.EnableNotifyIcon);

            BindEvent();
        }

        public override void Dispose()
        {
            base.Dispose();
            mainWindow.StateChanged -= MainWindow_StateChanged;
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
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

        public void Enable(bool isEnable)
        {
            Data.Settings.Current.EnableNotifyIcon = isEnable;
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = Data.Settings.Current.EnableNotifyIcon;
            }
        }

        void BindEvent()
        {
            mainWindow.StateChanged += MainWindow_StateChanged;
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
