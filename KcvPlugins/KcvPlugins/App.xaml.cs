﻿using Grabacr07.KanColleViewer.Composition;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ThemeService.Current.Initialize(this, Theme.Dark, Accent.Purple);

            this.Activated += App_Activated;
            this.Exit += App_Exit;

            PluginList = new List<IToolPlugin>();
            PluginList.Add(new AMing.SettingsExtensions.Entrance());
            PluginList.Add(new AMing.DebugExtensions.Entrance());

            this.MainWindow = new MainWindow();
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
