using Grabacr07.KanColleViewer.Composition;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.SettingsExtensions
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "SettingsExtensions")]
    [ExportMetadata("Description", "KCV Settings Extensions")]
    [ExportMetadata("Version", Entrance.IToolPluginVersion)]
    [ExportMetadata("Author", "@AMing")]
    public class Entrance : IToolPlugin
    {
        private readonly ViewModels.SettingsViewModel settingsViewModel = new ViewModels.SettingsViewModel();

        public const string IToolPluginVersion = "1.10.1";
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

            Data.Settings.Load();
        }

        private void Exit()
        {
            Data.Settings.Current.Save();
        }
    }
}
