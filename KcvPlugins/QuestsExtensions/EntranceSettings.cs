using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.QuestsExtensions
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "QuestsExtensions")]
    [ExportMetadata("Description", "KCV Quests Extensions")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("Author", "@AMing")]
    public class EntranceSettings : IToolPlugin
    {
        private readonly ViewModels.SettingsViewModel settingsViewModel = new ViewModels.SettingsViewModel();
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

        public EntranceSettings()
        {
            Init();
        }

        ~EntranceSettings()
        {
            Exit();
        }

        private void Init()
        {
            Data.Settings.FirstLoad();
        }

        private void Exit()
        {
            Data.Settings.Current.Save();
        }
    }
}
