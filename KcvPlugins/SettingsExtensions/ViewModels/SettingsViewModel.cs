using AMing.SettingsExtensions.Data;
using Livet;
using Livet.Commands;
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


        #region HotKey_KeyText

        private string _hotKey_KeyText;

        public string HotKey_KeyText
        {
            get
            {
                if (_hotKey_KeyText == null)
                {
                    HotKeyReset();
                }
                return _hotKey_KeyText;
            }
            set
            {
                if (_hotKey_KeyText != value)
                {
                    _hotKey_KeyText = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private bool hotkeyIsChange = false;

        public bool HotkeyIsChange
        {
            get { return hotkeyIsChange; }
            set
            {
                if (hotkeyIsChange != value)
                {
                    hotkeyIsChange = value;
                    this.RaisePropertyChanged();
                }
            }
        }



        #endregion

        private void UpdateHotKey_KeyText()
        {
            string modifierkey_txt = temp_ModifierKeys == ModifierKeys.None ? string.Empty : temp_ModifierKeys.ToString(),
                   key_txt = temp_Key == Key.None ? string.Empty : temp_Key.ToString();

            HotKey_KeyText = string.Format("{0}{1}{2}",
                modifierkey_txt,
                (temp_ModifierKeys == ModifierKeys.None || temp_Key == Key.None) ? string.Empty : " + ",
                key_txt
                );
            HotkeyIsChange = Data.Settings.Current.HotKey_ModifierKeys != temp_ModifierKeys || Data.Settings.Current.HotKey_Key != temp_Key;
        }

        #region event

        ModifierKeys temp_ModifierKeys = ModifierKeys.None;
        Key temp_Key = Key.None;
        public void HotKeyTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.None:
                case Key.System:
                case Key.LeftAlt:
                case Key.LeftCtrl:
                case Key.LeftShift:
                case Key.LWin:
                case Key.RightAlt:
                case Key.RightCtrl:
                case Key.RightShift:
                case Key.RWin:
                    return;
            }
            temp_ModifierKeys = e.KeyboardDevice.Modifiers;
            temp_Key = e.Key;

            UpdateHotKey_KeyText();
        }
        public void HotKeyConfirm()
        {
            Data.Settings.Current.HotKey_ModifierKeys = temp_ModifierKeys;
            Data.Settings.Current.HotKey_Key = temp_Key;
            Modules.HotKeyModules.Current.ResetHotKey();

            UpdateHotKey_KeyText();
        }
        public void HotKeyReset()
        {
            temp_ModifierKeys = Data.Settings.Current.HotKey_ModifierKeys;
            temp_Key = Data.Settings.Current.HotKey_Key;

            UpdateHotKey_KeyText();
        }

        #endregion
    }
}
