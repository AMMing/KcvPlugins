using AMing.Plugins.Core.Enums;
using AMing.Plugins.Core.Models;
using AMing.Plugins.Core.Modules;
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
        winforms.MenuItem exitItem;
        bool _notifyInit = false;

        public override void Initialize()
        {
            base.Initialize();
            mainWindow = Application.Current.MainWindow;

            InitNotifyIcon();
            ResetNotifyIconVisible();

            InitPublicModules();

            MessagerModules.Current.Register<WindowState>(this, AMing.Plugins.Core.StaticData.MessagerKey + "MainWindow_StateChanged", MainWindow_StateChanged);
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

                    //showhideItem = new winforms.MenuItem
                    //{
                    //    Text = string.Format("{1}{0}", TextResource.KanColleViewer, TextResource.Hide)
                    //};
                    exitItem = new winforms.MenuItem
                    {
                        Text = TextResource.NotifyIcon_ContextMenu_Exit
                    };
                    //showhideItem.Click += showhideItem_Click;
                    exitItem.Click += exitItem_Click;

                    //contextMenu.MenuItems.Add(showhideItem);
                    contextMenu.MenuItems.Add(exitItem);

                    _notifyIcon = new System.Windows.Forms.NotifyIcon
                    {
                        Text = GetTitle(),
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

        string GetTitle()
        {
            return string.Format("{2}{1}{0}", TextResource.KanColleViewer,
                (Application.Current.MainWindow.WindowState == WindowState.Minimized) ? TextResource.Show : TextResource.Hide,
                TextResource.DoubleClick);
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

        public List<ModulesItem> CurrentPublicModules { get; set; }

        void InitPublicModules()
        {
            if (_notifyIcon == null || !_notifyInit)
            {
                return;
            }
            CurrentPublicModules = new List<ModulesItem>();
            PublicModules.Current.PublicModulesList.ForEach(item => AddPublicModules(item));

            PublicModules.Current.ModulesChange += (sender, e) =>
            {
                if (e.Type == ModulesChangeEventArgsType.Add)
                {
                    e.ChangeList.ForEach(item => AddPublicModules(item));
                }
            };
        }

        void AddPublicModules(ModulesItem modulesItem)
        {
            if ((modulesItem.Type != ModulesType.Pubilc &&
                modulesItem.Type != ModulesType.NotifyIcon) ||
                CurrentPublicModules.Contains(modulesItem))
            {
                return;
            }
            CurrentPublicModules.Add(modulesItem);

            var menuItem = new winforms.MenuItem
            {
                Text = modulesItem.ModulesName,
                Tag = modulesItem.ModulesKey
            };
            menuItem.Click += (sender, e) => MessagerModules.Current.Send(modulesItem.MessageKey);
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
            _notifyIcon.Text = GetTitle();
        }

        void exitItem_Click(object sender, EventArgs e)
        {
            mainWindow.Close();
        }

        void _notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            var modulesitem = this.CurrentPublicModules.FirstOrDefault(item => item.ModulesKey ==  AMing.Plugins.Core.StaticData.PublicModulesKey + "ChangeAllWindowsByMainWindow");
            if (modulesitem != null)
            {
                MessagerModules.Current.Send(modulesitem.MessageKey);
            }
        }
        #endregion
    }
}
