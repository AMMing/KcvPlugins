using Grabacr07.KanColleViewer.Composition;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KcvPlugins
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

            Application.Current.LoadCompleted += Current_LoadCompleted;
            Application.Current.Navigated += Current_Navigated;
            Application.Current.Navigating += Current_Navigating;
            
        }  


        static void Current_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        static void Current_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        static void Current_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            throw new NotImplementedException();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ThemeService.Current.Initialize(this, Theme.Dark, Accent.Purple);

            this.Activated += App_Activated;
            this.Exit += App_Exit;

            //PluginList = new List<IToolPlugin>();
            //PluginList.Add(new AMing.SettingsExtensions.Entrance());
            //PluginList.Add(new AMing.SettingsExtensions.Entrance_keys());
            ////PluginList.Add(new AMing.QuestsExtensions.Entrance());
            ////PluginList.Add(new AMing.QuestsExtensions.EntranceSettings());
            //PluginList.Add(new AMing.DebugExtensions.Entrance());

            //this.MainWindow = new MainWindow();
            //this.MainWindow.Show();
            //模拟kcv的mainwindow
            //this.MainWindow = new KcvSimulationWindow();
            //this.MainWindow.Show();
            this.MainWindow = new ToastWindow();
            this.MainWindow.Show();
        }
        public static List<IToolPlugin> PluginList { get; set; }

        void App_Exit(object sender, ExitEventArgs e)
        {
            //MessageBox.Show("App_Exit");
        }

        void App_Activated(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("App_Activated");
        }

    }
}
