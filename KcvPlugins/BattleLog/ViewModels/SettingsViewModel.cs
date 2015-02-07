using AMing.Logger.Data;
using Livet;
using Livet.Commands;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.Logger.ViewModels
{
    public class SettingsViewModel : AMing.Plugins.Core.ViewModels.ViewModelEx
    {
        public SettingsViewModel()
        {
            #region init

            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            PluginInfo = string.Format("{0} Version {1}",
                assembly.Name,
                assembly.Version.ToString());

            #endregion
        }

        #region PluginInfo

        private string _pluginInfo;

        public string PluginInfo
        {
            get { return _pluginInfo; }
            set { base.RaisePropertyChanged(ref _pluginInfo, value); }
        }

        #endregion




    }
}
