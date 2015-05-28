using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.Plugins.Base
{
    [Export(typeof(Grabacr07.KanColleViewer.Composition.IToolPlugin))]
    [ExportMetadata("Title", "AMing Plugins Base")]
    [ExportMetadata("Description", "KCV Plugins")]
    [ExportMetadata("Version", Entrance.IToolPluginVersion)]
    [ExportMetadata("Author", "@AMing")]
    public class Entrance : Grabacr07.KanColleViewer.Composition.IToolPlugin
    {
        private readonly ViewModels.SettingsViewModel settingsViewModel = new ViewModels.SettingsViewModel();

        public const string IToolPluginVersion = "1.0";
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
        private void Init()
        {
            Hosting.PluginHost.Current.Initialize();

        }

        private void Exit()
        {
            Generic.SettingsHelper.Current.SaveAll();
            Generic.ModulesHelper.Current.DisposeModulesList();
        }

    }
}