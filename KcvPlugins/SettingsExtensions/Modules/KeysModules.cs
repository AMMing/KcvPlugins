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
    public class KeysModules : ModulesBase
    {
        #region Current

        private static KeysModules _current = new KeysModules();

        public static KeysModules Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public Data.KeySetting Setting { get; set; }


        public override void Initialize()
        {
            base.Initialize();

            Data.KeySetting.Current.Load();
            Setting = Data.KeySetting.Current;

            InitPublicModules();
        }


        public Models.KeyModulesItem GetKeyModulesItem(Models.ModulesItem modulesItem)
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

        public Models.KeyModulesItem GetKeyModulesItem(Models.KeySetting keySetting)
        {
            var key = keySetting.ModulesKey;
            var modulesItem = this.CurrentPublicModules.FirstOrDefault(item => item.ModulesKey == key);
            if (modulesItem == null)
            {
                return null;
            }
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
                Setting.AddOrUpdate(keysetting);

                Generic.HotKeyHelper.Current.Set(keyModulesItem);
                MainWindowModules.Current.SetKeys(keyModulesItem);

            }
            OnKeysPublicModulesChange();
        }

        #region PublicModules

        public List<Models.ModulesItem> CurrentPublicModules { get; set; }


        public event EventHandler<List<Models.ModulesItem>> KeysPublicModulesChange;

        private void OnKeysPublicModulesChange()
        {
            if (KeysPublicModulesChange != null)
            {
                KeysPublicModulesChange(this, this.CurrentPublicModules);
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
                OnKeysPublicModulesChange();
            };
            OnKeysPublicModulesChange();
        }

        void AddPublicModules(Models.ModulesItem modulesItem)
        {
            if ((modulesItem.Type != Enums.ModulesType.Pubilc &&
                modulesItem.Type != Enums.ModulesType.Keys) ||
                CurrentPublicModules.Contains(modulesItem))
            {
                return;
            }
            CurrentPublicModules.Add(modulesItem);

            var keyModulesItem = GetKeyModulesItem(modulesItem);
            if (keyModulesItem != null)
            {
                try
                {
                    Generic.HotKeyHelper.Current.Set(keyModulesItem);
                    MainWindowModules.Current.SetKeys(keyModulesItem);
                }
                catch (NotImplementedException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception)
                {
                }
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