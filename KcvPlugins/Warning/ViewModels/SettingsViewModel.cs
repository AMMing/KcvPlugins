using Livet;
using Livet.Commands;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Warning.ViewModels
{
    public class SettingsViewModel : ViewModel
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
            set
            {
                if (_pluginInfo != value)
                {
                    _pluginInfo = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

       

    }
}
