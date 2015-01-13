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
        winforms.ContextMenu contextMenu;
        winforms.MenuItem showhideItem, exitItem;
        bool _notifyInit = false;

        public override void Initialize()
        {
            base.Initialize();
            mainWindow = Application.Current.MainWindow;

            InitNotifyIcon();
            ResetNotifyIconVisible();

            InitPublicModules();

            Modules.Generic.MessagerHelper.Current.Register<WindowState>(this, Entrance.MessagerKey + "MainWindow_StateChanged", MainWindow_StateChanged);
        }

        public override void Dispose()
        {
            base.Dispose();
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
            }
        }

        #region method

        void InitNotifyIcon()
        {
            try
            {
#if DEBUG
                //if (mainWindow.Icon != null &&
                //    Data.Settings.Current.NotifyIcon_Path != mainWindow.Icon.ToString())
                //{
                //    Data.Settings.Current.NotifyIcon_Path = mainWindow.Icon.ToString();
                //}
#endif
                Data.Settings.Current.NotifyIcon_Path = Data.Settings.Current.NotifyIcon_Path ??
                                                        Data.Settings.DefaultNotifyIconPath;
                var iconPath = Data.Settings.Current.NotifyIcon_Path;

                Uri iconUri = new Uri(iconPath, UriKind.Absolute);
                using (var icon_stream = Application.GetResourceStream(iconUri).Stream)
                {
                    contextMenu = new winforms.ContextMenu();

                    showhideItem = new winforms.MenuItem
                    {
                        Text = string.Format("{1}{0}", TextResource.KanColleViewer, TextResource.Hide)
                    };
                    exitItem = new winforms.MenuItem
                    {
                        Text = TextResource.NotifyIcon_ContextMenu_Exit
                    };
                    showhideItem.Click += showhideItem_Click;
                    exitItem.Click += exitItem_Click;

                    contextMenu.MenuItems.Add(showhideItem);
                    contextMenu.MenuItems.Add(exitItem);

                    _notifyIcon = new System.Windows.Forms.NotifyIcon
                    {
                        Text = string.Format("{0}{1}", TextResource.DoubleClick, showhideItem.Text),
                        Icon = new Icon(icon_stream),
                        ContextMenu = contextMenu
                    };
                    _notifyIcon.DoubleClick += _notifyIcon_DoubleClick;
                }


                _notifyInit = true;
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 根据配置重新设置NotifyIcon的Visible值
        /// </summary>
        public void ResetNotifyIconVisible()
        {
            if (_notifyIcon == null || !_notifyInit)
            {
                return;
            }
            _notifyIcon.Visible = Data.Settings.Current.EnableNotifyIcon;
        }

        #region PublicModules

        public List<string> CurrentPublicModules { get; set; }

        void InitPublicModules()
        {
            if (_notifyIcon == null || !_notifyInit)
            {
                return;
            }
            CurrentPublicModules = new List<string>();
            Modules.Generic.PublicModules.Current.PublicModulesList.ForEach(item => AddPublicModules(item));

            Modules.Generic.PublicModules.Current.ModulesChange += (sender, e) =>
            {
                if (e.Type == Enums.ModulesChangeEventArgsType.Add)
                {
                    e.ChangeList.ForEach(item => AddPublicModules(item));
                }
            };
        }

        void AddPublicModules(Models.ModulesItem modulesItem)
        {
            if ((modulesItem.Type != Enums.ModulesType.Pubilc && modulesItem.Type != Enums.ModulesType.NotifyIcon) || CurrentPublicModules.Contains(modulesItem.ModulesKey))
            {
                return;
            }
            var menuItem = new winforms.MenuItem
            {
                Text = modulesItem.ModulesName,
                Tag = modulesItem.ModulesKey
            };
            menuItem.Click += (sender, e) => Modules.Generic.MessagerHelper.Current.Send(modulesItem.MessageKey);
            modulesItem.RegisterEnabelChangeCallbck(isenabel => menuItem.Enabled = isenabel);

            contextMenu.MenuItems.Add(menuItem);
            //-,-为了将退出项移到最后一个
            contextMenu.MenuItems.Remove(exitItem);
            contextMenu.MenuItems.Add(exitItem);
        }
        #endregion

        #endregion

        #region event

        void MainWindow_StateChanged(WindowState val)
        {
            showhideItem.Text = string.Format("{1}{0}", TextResource.KanColleViewer, (val == WindowState.Minimized) ? TextResource.Show : TextResource.Hide);
            _notifyIcon.Text = string.Format("{0}{1}", TextResource.DoubleClick, showhideItem.Text);
        }

        void showhideItem_Click(object sender, EventArgs e)
        {
            Modules.Generic.MessagerHelper.Current.Send(Entrance.MessagerKey + "ShowHideWindow");
        }
        void exitItem_Click(object sender, EventArgs e)
        {
            mainWindow.Close();
        }

        void _notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            showhideItem_Click(showhideItem, e);
        }
        #endregion
    }
}
