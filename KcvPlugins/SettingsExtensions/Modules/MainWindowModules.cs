using AMing.SettingsExtensions.Helper;
using Grabacr07.Desktop.Metro.Controls;
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
        }

        #region method

        Helper.WindowStateHelper WindowStateHelper = new WindowStateHelper();
        #endregion

        public override void Initialize()
        {
            base.Initialize();

            var mainWindow = Application.Current.MainWindow;

            WindowStateHelper.Init(mainWindow);
            mainWindow.StateChanged += (sender, e) => Modules.MessagerModules.Current.Send<WindowState>(Entrance.MessagerKey + "MainWindow_StateChanged", mainWindow.WindowState);

            InitPublicModules();
        }

        #region PublicModules

        private void InitPublicModules()
        {
            var modulesItem_HideAllWindows = new Models.ModulesItem(this, "HideAllWindows", string.Format("{0}{1}", TextResource.Hide, TextResource.AllWindow));
            modulesItem_HideAllWindows.Register(HideAllWindows);
            PublicModules.Current.Add(modulesItem_HideAllWindows);

            var modulesItem_ShowAllWindows = new Models.ModulesItem(this, "ShowAllWindows", string.Format("{0}{1}", TextResource.Show, TextResource.AllWindow));
            modulesItem_ShowAllWindows.Register(ShowAllWindows);
            PublicModules.Current.Add(modulesItem_ShowAllWindows);

            var modulesItem_ChangeAllWindowsByMainWindow = new Models.ModulesItem(this, "ChangeAllWindowsByMainWindow", string.Format("{0}/{1}{2}", TextResource.Show, TextResource.Hide, TextResource.AllWindow));
            modulesItem_ChangeAllWindowsByMainWindow.Register(ChangeAllWindowsByMainWindow);
            PublicModules.Current.Add(modulesItem_ChangeAllWindowsByMainWindow);
        }

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
                        win.Hide();
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
                        win.Show();
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

        #endregion

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
