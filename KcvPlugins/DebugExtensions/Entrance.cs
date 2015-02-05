using Grabacr07.KanColleViewer.Composition;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.DebugExtensions
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "DebugExtensions")]
    [ExportMetadata("Description", "KCV Debug Extensions")]
    [ExportMetadata("Version", "1.1")]
    [ExportMetadata("Author", "@AMing")]
    public class Entrance : IToolPlugin
    {
        public readonly ViewModels.SettingsViewModel settingsViewModel = new ViewModels.SettingsViewModel();
        public string ToolName
        {
            get { return TextResource.Plugin_ToolName; }
        }

        public object GetToolView()
        {
            return new Views.SettingsControl { DataContext = this.settingsViewModel };
        }

        public object GetSettingsView()
        {
            return null;
        }

        public Entrance()
        {
            Init();
        }

        ~Entrance()
        {
            Exit();
        }

        Modules.InitModules initModules;
        private void Init()
        {
            initModules = new Modules.InitModules();
            initModules.Initialize();
            Modules.LogsModules.Current.SettingsViewModel = this.settingsViewModel;

            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Helper.ErrorLogHelper.Current.Append(e.Exception);
            //#if DEBUG
            MessageBox.Show(string.Format("Error:{0}\n{1}\n{2}",
                e.Exception.Message,
                e.Exception.Source,
                e.Exception.StackTrace));
            //#endif
            e.Handled = true;
        }

        private void Exit()
        {
        }
    }
}
