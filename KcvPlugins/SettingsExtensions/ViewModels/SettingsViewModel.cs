using AMing.SettingsExtensions.Data;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AMing.SettingsExtensions.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        public SettingsViewModel()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            PluginInfo = string.Format("{0} Version {1} ",
                assembly.Name,
                assembly.Version.ToString());
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

        #region EnableExitTip

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

        #region EnableNotifyIcon

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

        #region EnableWindowMiniHideTaskbar

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


        #region EnableHotKeyShowHide

        public bool EnableHotKeyShowHide
        {
            get { return Settings.Current.EnableHotKeyShowHide; }
            set
            {
                if (Settings.Current.EnableHotKeyShowHide != value)
                {
                    Settings.Current.EnableHotKeyShowHide = value;
                    this.RaisePropertyChanged();
                    Modules.HotKeyModules.Current.ResetHotKey();
                }
            }
        }

        #endregion


        #region HotKey_Key

        public string HotKey_Key
        {
            get { return Settings.Current.HotKey_Key.ToString(); }
            set
            {
                Key key;
                if (Enum.TryParse<Key>(value, out key) &&
                    Settings.Current.HotKey_Key != key)
                {
                    Settings.Current.HotKey_Key = key;
                    this.RaisePropertyChanged();
                    Modules.HotKeyModules.Current.ResetHotKey();
                }
            }
        }

        #endregion

        #region HotKey_ModifierKeys

        public string HotKey_ModifierKeys
        {
            get { return Settings.Current.HotKey_ModifierKeys.ToString(); }
            set
            {
                ModifierKeys key;
                if (Enum.TryParse<ModifierKeys>(value, out key) &&
                    Settings.Current.HotKey_ModifierKeys != key)
                {
                    Settings.Current.HotKey_ModifierKeys = key;
                    this.RaisePropertyChanged();
                    Modules.HotKeyModules.Current.ResetHotKey();
                }
            }
        }

        #endregion
    }
}
