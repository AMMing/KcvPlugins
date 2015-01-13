using AMing.SettingsExtensions.Helper;
using Grabacr07.Desktop.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using kcv = Grabacr07.KanColleViewer;

namespace AMing.SettingsExtensions.Modules
{
    public class HotKeysModules : ModulesBase
    {
        #region Current

        private static HotKeysModules _current = new HotKeysModules();

        public static HotKeysModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public Data.KeySetting Setting { get; set; }


        public override void Initialize()
        {
            base.Initialize();

            Data.KeySetting.Load();
            Setting = Data.KeySetting.Current;

            InitPublicModules();
        }


        private Models.KeyModulesItem GetKeyModulesItem(Models.ModulesItem modulesItem)
        {
            var key = modulesItem.ModulesKey;
            var keySetting = Setting.Get(key) ??
                new Models.KeySetting
                {
                    ModulesKey = key,
                    IsNotSetKey = true
                };

            return new Models.KeyModulesItem
            {
                ModulesItem = modulesItem,
                KeySetting = keySetting
            };
        }

        private Models.KeyModulesItem GetKeyModulesItem(Models.KeySetting keySetting)
        {
            var key = keySetting.ModulesKey;
            var modulesItem = this.CurrentPublicModules.FirstOrDefault(item => item.ModulesKey == key);
            if (modulesItem == null)
            {
                return null;
            }
            keySetting.Type = Enums.KeyType.HotKey;
            return new Models.KeyModulesItem
            {
                ModulesItem = modulesItem,
                KeySetting = keySetting
            };
        }
        public void SetKey(Models.KeySetting keysetting)
        {
            var keyModulesItem = GetKeyModulesItem(keysetting);
            if (keyModulesItem != null)
            {
                Generic.HotKeyHelper.Current.Set(keyModulesItem);
            }
        }

        #region PublicModules

        public List<Models.ModulesItem> CurrentPublicModules { get; set; }


        public event EventHandler<List<Models.ModulesItem>> HotkeyPublicModulesChange;

        private void OnHotkeyPublicModulesChange()
        {
            if (HotkeyPublicModulesChange != null)
            {
                HotkeyPublicModulesChange(this, this.CurrentPublicModules);
            }
        }

        void InitPublicModules()
        {
            CurrentPublicModules = new List<Models.ModulesItem>();
            Modules.PublicModules.Current.PublicModulesList.ForEach(item => AddPublicModules(item));

            Modules.PublicModules.Current.ModulesChange += (sender, e) =>
            {
                if (e.Type == Enums.ModulesChangeEventArgsType.Add)
                {
                    e.ChangeList.ForEach(item => AddPublicModules(item));
                }
                OnHotkeyPublicModulesChange();
            };
            OnHotkeyPublicModulesChange();
        }

        void AddPublicModules(Models.ModulesItem modulesItem)
        {
            if ((modulesItem.Type != Enums.ModulesType.Pubilc &&
                modulesItem.Type != Enums.ModulesType.HotKey) ||
                CurrentPublicModules.Contains(modulesItem))
            {
                return;
            }
            CurrentPublicModules.Add(modulesItem);

            var keyModulesItem = GetKeyModulesItem(modulesItem);
            if (keyModulesItem != null)
            {
                Generic.HotKeyHelper.Current.Set(keyModulesItem);
            }
        }
        #endregion

        public override void Dispose()
        {
            base.Dispose();
            Data.KeySetting.Current.Save();
        }
    }
}
//提供 modules name 和 key 在 keysetting 界面让用户选择设置，然后传到这边会判断是否存在key 然后进行添加或者修改