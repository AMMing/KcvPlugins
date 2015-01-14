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
    public class Entrance_keys : IToolPlugin
    {
        private readonly ViewModels.KeysSettingsViewModel keysSettingsViewModel = new ViewModels.KeysSettingsViewModel();
        public string ToolName
        {
            get { return TextResource.Plugin_ToolName_keys; }
        }

        public object GetToolView()
        {
            return new Views.KeysSettingsControl { DataContext = this.keysSettingsViewModel };
        }

        public object GetSettingsView()
        {
            return null;
        }

        public Entrance_keys()
        {
            Init();
        }

        ~Entrance_keys()
        {
            Exit();
        }

        private void Init()
        {
        }

        private void Exit()
        {
        }
    }
}
