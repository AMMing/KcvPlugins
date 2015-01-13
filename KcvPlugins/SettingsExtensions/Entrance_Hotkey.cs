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
    public class Entrance_Hotkey : IToolPlugin
    {
        private readonly ViewModels.SettingsViewModel settingsViewModel = new ViewModels.SettingsViewModel();
        public string ToolName
        {
            get { return TextResource.Plugin_ToolName_Hotkey; }
        }

        public object GetToolView()
        {
            return new Views.SettingsControl { DataContext = this.settingsViewModel };
        }

        public object GetSettingsView()
        {
            return null;
        }

        public Entrance_Hotkey()
        {
            Init();
        }

        ~Entrance_Hotkey()
        {
            Exit();
        }

        private void Init()
        {
            Modules.InitModules.Current.Initialize();
            Data.Settings.Load();
        }

        private void Exit()
        {
            Data.Settings.Current.Save();
        }
    }
}
