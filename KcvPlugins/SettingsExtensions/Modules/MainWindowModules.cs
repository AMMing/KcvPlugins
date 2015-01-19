using AMing.SettingsExtensions.Helper;
using Grabacr07.Desktop.Metro.Controls;
using Grabacr07.KanColleViewer.ViewModels;
using Grabacr07.KanColleViewer.ViewModels.Contents;
using Livet.Behaviors;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.SettingsExtensions.Modules
{
    public class MainWindowModules : ModulesBase
    {
        #region Current

        private static MainWindowModules _current = new MainWindowModules();

        public static MainWindowModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public MainWindowModules()
        {
            CurrentKeyModulesItem = new Dictionary<string, Models.KeyModulesItem>();
        }

        #region method

        Helper.WindowStateHelper WindowStateHelper = new WindowStateHelper();
        public Dictionary<string, Models.KeyModulesItem> CurrentKeyModulesItem { get; set; }
        #endregion

        public override void Initialize()
        {
            base.Initialize();

            var mainWindow = Application.Current.MainWindow;

            WindowStateHelper.Init(mainWindow);
            InitPublicModules();

            mainWindow.StateChanged += (sender, e) => Modules.MessagerModules.Current.Send<WindowState>(Entrance.MessagerKey + "MainWindow_StateChanged", mainWindow.WindowState);
            mainWindow.KeyDown += (sender, e) => Run(e.KeyboardDevice.Modifiers, e.Key);
        }

        #region PublicModules

        private void InitPublicModules()
        {
            //隐藏全部窗体
            var modulesItem_HideAllWindows = new Models.ModulesItem(this, PublicModulesKeys.HideAllWindows, string.Format("{0}{1}", TextResource.Hide, TextResource.AllWindow));
            modulesItem_HideAllWindows.Type = Enums.ModulesType.Keys;
            modulesItem_HideAllWindows.Register(HideAllWindows);
            PublicModules.Current.Add(modulesItem_HideAllWindows);
            //显示全部窗体
            var modulesItem_ShowAllWindows = new Models.ModulesItem(this, PublicModulesKeys.ShowAllWindows, string.Format("{0}{1}", TextResource.Show, TextResource.AllWindow));
            modulesItem_ShowAllWindows.Type = Enums.ModulesType.Keys;
            modulesItem_ShowAllWindows.Register(ShowAllWindows);
            PublicModules.Current.Add(modulesItem_ShowAllWindows);
            //显示隐藏全部窗体
            var modulesItem_ChangeAllWindowsByMainWindow = new Models.ModulesItem(this, PublicModulesKeys.ChangeAllWindowsByMainWindow, string.Format("{0}/{1}{2}", TextResource.Show, TextResource.Hide, TextResource.AllWindow));
            modulesItem_ChangeAllWindowsByMainWindow.Register(ChangeAllWindowsByMainWindow);
            PublicModules.Current.Add(modulesItem_ChangeAllWindowsByMainWindow);

            //切换tabs
            var modulesItem_ChangeTabs = new Models.ModulesItem(this, PublicModulesKeys.ChangeTabs, TextResource.ChangeTabs);
            modulesItem_ChangeTabs.Type = Enums.ModulesType.Keys;
            modulesItem_ChangeTabs.Register(WindowViewModules.Current.WindowViewHelper.ChangeTabs);
            PublicModules.Current.Add(modulesItem_ChangeTabs);

            //开关声音
            var modulesItem_ToggleMute = new Models.ModulesItem(this, PublicModulesKeys.ToggleMute, TextResource.ToggleMute);
            modulesItem_ToggleMute.Type = Enums.ModulesType.Keys;
            modulesItem_ToggleMute.Register(ToggleMute);
            PublicModules.Current.Add(modulesItem_ToggleMute);
            //截图
            var modulesItem_TakeScreenshot = new Models.ModulesItem(this, PublicModulesKeys.TakeScreenshot, TextResource.Screenshot);
            modulesItem_TakeScreenshot.Type = Enums.ModulesType.Keys;
            modulesItem_TakeScreenshot.Register(TakeScreenshot);
            PublicModules.Current.Add(modulesItem_TakeScreenshot);

            //舰娘一览
            var modulesItem_ShowShipCatalog = new Models.ModulesItem(this, PublicModulesKeys.ShowShipCatalog, TextResource.ShowShipCatalog);
            modulesItem_ShowShipCatalog.Register(ShowShipCatalog);
            PublicModules.Current.Add(modulesItem_ShowShipCatalog);
            //装备一览
            var modulesItem_ShowSlotItemCatalog = new Models.ModulesItem(this, PublicModulesKeys.ShowSlotItemCatalog, TextResource.ShowSlotItemCatalog);
            modulesItem_ShowSlotItemCatalog.Register(ShowSlotItemCatalog);
            PublicModules.Current.Add(modulesItem_ShowSlotItemCatalog);
        }
        private readonly MethodBinder binder = new MethodBinder();

        void HideAllWindows()
        {
            if (Application.Current.Windows != null)
            {
                foreach (var item in Application.Current.Windows)
                {
                    var win = item as Window;
                    if (win != null && win.IsInitialized)
                    {
                        Helper.WindowStateHelper.WindowShowHideForTaskBar(win, false);
                        win.WindowState = WindowState.Minimized;
                    }
                }
            }
        }
        void ShowAllWindows()
        {
            if (Application.Current.Windows != null)
            {
                foreach (var item in Application.Current.Windows)
                {
                    var win = item as Window;
                    if (win != null && win.IsInitialized)
                    {
                        Helper.WindowStateHelper.WindowShowHideForTaskBar(win, true);
                        win.WindowState = WindowStateHelper.OldwinState;
                    }
                }
            }
        }

        void ChangeAllWindowsByMainWindow()
        {
            var winState = WindowStateHelper.ShowHideWindow();
            if (Application.Current.Windows != null && winState.HasValue)
            {
                foreach (var item in Application.Current.Windows)
                {
                    var win = item as Window;
                    if (win != null && win.IsInitialized)
                    {
                        win.WindowState = winState.Value;
                        Helper.WindowStateHelper.WindowShowHideForTaskBar(win, winState != WindowState.Minimized);
                    }
                }
            }
        }

        void ToggleMute()
        {
            try
            {
                if (Application.Current.MainWindow == null ||
                    Application.Current.MainWindow.DataContext == null)
                    return;

                var mainWindowViewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;
                if (mainWindowViewModel == null)
                    return;
                var mainContentViewModel = mainWindowViewModel.Content as MainContentViewModel;
                if (mainContentViewModel == null || mainContentViewModel.Volume == null)
                    return;

                mainContentViewModel.Volume.ToggleMute();
            }
            catch (Exception ex)
            {
                MessageBoxDialog.Show(ex.Message);
            }
        }

        void TakeScreenshot()
        {
            try
            {
                if (Application.Current.MainWindow == null ||
                    Application.Current.MainWindow.DataContext == null)
                    return;

                var mainWindowViewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;
                if (mainWindowViewModel == null)
                    return;

                mainWindowViewModel.TakeScreenshot();
            }
            catch (Exception ex)
            {
                MessageBoxDialog.Show(ex.Message);
            }
        }

        OverviewViewModel GetOverviewViewModel()
        {
            if (Application.Current.MainWindow == null ||
                  Application.Current.MainWindow.DataContext == null)
                return null;

            var mainWindowViewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;
            if (mainWindowViewModel == null)
                return null;
            var mainContentViewModel = mainWindowViewModel.Content as MainContentViewModel;
            if (mainContentViewModel == null || mainContentViewModel.TabItems == null || mainContentViewModel.TabItems.Count == 0)
                return null;

            return mainContentViewModel.TabItems[0] as OverviewViewModel;
        }

        void ShowShipCatalog()
        {
            try
            {
                var overviewViewModel = GetOverviewViewModel();
                if (overviewViewModel != null)
                {
                    overviewViewModel.ShowShipCatalog();
                }
            }
            catch (Exception ex)
            {
                MessageBoxDialog.Show(ex.Message);
            }
        }
        void ShowSlotItemCatalog()
        {
            try
            {
                var overviewViewModel = GetOverviewViewModel();
                if (overviewViewModel != null)
                {
                    overviewViewModel.ShowSlotItemCatalog();
                }
            }
            catch (Exception ex)
            {
                MessageBoxDialog.Show(ex.Message);
            }
        }
        
        #endregion

        #region WindowKeys

        private void Run(ModifierKeys modifierKeys, Key key)
        {
            var result = this.CurrentKeyModulesItem.Where(item =>
            {
                return item.Value.KeySetting.ModifierKeys == modifierKeys && item.Value.KeySetting.Key == key;
            });

            if (result != null)
            {
                foreach (var item in result)
                {
                    if (item.Value.KeySetting.Type != Enums.KeyType.Normal) return;
                    Modules.MessagerModules.Current.Send(item.Value.ModulesItem.MessageKey);
                }
            }
        }



        public void SetKeys(Models.KeyModulesItem item)
        {
            if (item.ModulesIsInvalid) return;

            string key = item.ModulesItem.ModulesKey;
            if (this.CurrentKeyModulesItem.ContainsKey(key))
            {
                this.CurrentKeyModulesItem[key] = item;
            }
            else
            {
                this.CurrentKeyModulesItem.Add(key, item);
            }
        }
        #endregion


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
