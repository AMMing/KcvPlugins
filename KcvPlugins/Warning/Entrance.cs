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
using System.Windows.Forms;
using kcv = Grabacr07.KanColleViewer;

namespace Warning
{
    [Export(typeof(IToolPlugin))]
    [ExportMetadata("Title", "Warning")]
    [ExportMetadata("Description", "Feet Hp Warning")]
    [ExportMetadata("Version", Entrance.IToolPluginVersion)]
    [ExportMetadata("Author", "@AMing")]
    public class Entrance : IToolPlugin
    {
        private readonly ViewModels.SettingsViewModel settingsViewModel = new ViewModels.SettingsViewModel();
        private readonly ViewModels.FleetsViewModel fleetsViewModel = new ViewModels.FleetsViewModel();


        public const string IToolPluginVersion = "1.0";
        public string ToolName
        {
            get
            {
                return "Warning";
                //return TextResource.Plugin_ToolName;
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

        private void Init()
        {
            //Modules.InitModules.Current.Initialize();
            //Data.Settings.Load();


        }

        private void Exit()
        {
            //Data.Settings.Current.Save();
        }
    }
   
}
