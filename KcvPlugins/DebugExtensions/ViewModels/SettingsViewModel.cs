using Livet;
using Livet.Commands;
using MetroRadiance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.DebugExtensions.ViewModels
{
    public class SettingsViewModel : ViewModel
    {

        #region PluginInfo

        private string _logsText;

        public string LogsText
        {
            get { return _logsText; }
            set
            {
                if (_logsText != value)
                {
                    _logsText = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion


        public void ClearLogs()
        {
            this.LogsText = string.Empty;
        }


    }
}
