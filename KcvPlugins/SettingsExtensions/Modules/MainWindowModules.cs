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
            mainWindow.StateChanged += (sender, e) => Modules.Generic.MessagerHelper.Current.Send<WindowState>(Entrance.MessagerKey + "MainWindow_StateChanged", mainWindow.WindowState);
            Modules.Generic.MessagerHelper.Current.Register(this, Entrance.MessagerKey + "ShowHideWindow", WindowStateHelper.ShowHideWindow);
        }

        #region event


        #endregion

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
