using AMing.Plugins.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.SettingsExtensions.Modules.Generic
{
    public class HotKeyHelper
    {
        #region Current

        private static HotKeyHelper _current = new HotKeyHelper();

        public static HotKeyHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region member
        public Dictionary<string, Helper.HotKeyHelper> CurrentHotKeyHelper { get; set; }

        #endregion

        #region method

        private Helper.HotKeyHelper GetKeyModulesItem(Models.KeyModulesItem keyModulesItem)
        {
            var key = keyModulesItem.ModulesItem.ModulesKey;
            if (CurrentHotKeyHelper.ContainsKey(key))
            {
                return CurrentHotKeyHelper[key];
            }
            else
            {
                var hotKeyHelper = new Helper.HotKeyHelper(Application.Current.MainWindow);
                hotKeyHelper.HotKeyDown += (sender, e) => MessagerModules.Current.Send(keyModulesItem.ModulesItem.MessageKey);
                CurrentHotKeyHelper.Add(key, hotKeyHelper);

                return hotKeyHelper;
            }
        }


        public void Set(Models.KeyModulesItem item)
        {
            if (item.ModulesIsInvalid) return;

            item.HotKeyHelper = GetKeyModulesItem(item);

            if (item.KeySetting.IsNotSetKey || item.KeySetting.Type != Enums.KeyType.HotKey)
            {
                UnregisterHotKey(item);
            }
            else
            {
                RegisterHotKey(item);
            }
        }


        void RegisterHotKey(Models.KeyModulesItem item)
        {
            if (item.KeySetting.IsNotSetKey) return;

            item.HotKeyHelper.Register(
                item.KeySetting.ModifierKeys,
                item.KeySetting.Key);

        }
        void UnregisterHotKey(Models.KeyModulesItem item)
        {
            item.HotKeyHelper.Unregister();
        }

        #endregion

        public HotKeyHelper()
        {
            CurrentHotKeyHelper = new Dictionary<string, Helper.HotKeyHelper>();
        }

        ~HotKeyHelper()
        {
            Helper.HotKeyHelper.UnregisterAll();
        }
    }
}
