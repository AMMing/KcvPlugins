using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Data.KeySetting Setting { get; set; }

        public event EventHandler<Models.ModulesChangeEventArgs> ModulesChange;

        private void OnModulesChange(Models.ModulesChangeEventArgs args)
        {
            if (ModulesChange != null)
                ModulesChange(this, args);
        }

        #endregion
        public HotKeyHelper()
        {
            Data.KeySetting.Load();
            Setting = Data.KeySetting.Current;
        }
        ~HotKeyHelper()
        {
            Data.KeySetting.Current.Save();
        }

        #region method


        public void Init()
        {
            foreach (var item in Setting.KeySettingList)
            {
                switch (item.Type)
                {
                    case AMing.SettingsExtensions.Enums.KeyType.Normal:
                        InitNormalkey(item);
                        break;
                    case AMing.SettingsExtensions.Enums.KeyType.HotKey:
                        InitHotkey(item);
                        break;
                    default:
                        break;
                }
            }
        }
        private void InitNormalkey(Models.KeySetting key)
        {

        }
        private void InitHotkey(Models.KeySetting key)
        {

        }

        #endregion
    }
}
