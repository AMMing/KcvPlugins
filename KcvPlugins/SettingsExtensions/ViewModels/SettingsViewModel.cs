using AMing.SettingsExtensions.Data;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        public SettingsViewModel()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            PluginInfo = string.Format("{0}[{1}]   Version {2}   @AMing",
                TextResource.Plugin_ToolName,
                assembly.Name,
                assembly.Version.ToString());
        }
        #region PluginInfo 変更通知

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

        #region EnableExitTip 変更通知

        public bool EnableExitTip
        {
            get { return Settings.Current.EnableExitTip; }
            set
            {
                if (Settings.Current.EnableExitTip != value)
                {
                    Settings.Current.EnableExitTip = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region EnableNotifyIcon 変更通知

        public bool EnableNotifyIcon
        {
            get { return Settings.Current.EnableNotifyIcon; }
            set
            {
                if (Settings.Current.EnableNotifyIcon != value)
                {
                    Settings.Current.EnableNotifyIcon = value;
                    Modules.NotifyIconModules.Current.ResetNotifyIconVisible();
                    if (!Settings.Current.EnableNotifyIcon)
                    {
                        EnableWindowMiniHideTaskbar = false;
                    }
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region EnableWindowMiniHideTaskbar 変更通知

        public bool EnableWindowMiniHideTaskbar
        {
            get { return Settings.Current.EnableWindowMiniHideTaskbar; }
            set
            {
                if (Settings.Current.EnableWindowMiniHideTaskbar != value)
                {
                    Settings.Current.EnableWindowMiniHideTaskbar = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion
    }
}
