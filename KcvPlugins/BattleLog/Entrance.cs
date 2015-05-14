using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.Logger
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "Logger")]
    [ExportMetadata("Description", "Logger")]
    [ExportMetadata("Version", Entrance.IToolPluginVersion)]
    [ExportMetadata("Author", "@AMing")]
    public class Entrance : IToolPlugin
    {
        private readonly ViewModels.SettingsViewModel settingsViewModel = new ViewModels.SettingsViewModel();


        public const string IToolPluginVersion = "1.2";
        public string ToolName
        {
            get
            {
                return TextResource.Plugin_ToolName;
            }
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
            Modules.LoggerModules.Current.SettingsViewModel = this.settingsViewModel;

            Data.Settings.Load();
        }

        private void Exit()
        {
            Data.Settings.Current.Save();
        }
    }

}
