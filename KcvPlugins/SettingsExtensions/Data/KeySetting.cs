using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Grabacr07.KanColleViewer.Models.Data.Xml;
using Grabacr07.KanColleWrapper;
using Livet;
using Grabacr07.KanColleViewer.Models;
using System.Windows.Input;
using MetroRadiance;

namespace AMing.SettingsExtensions.Data
{
    [Serializable]
    public class KeySetting
    {
        #region static members

#if DEBUG
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "SettingsExtensions.Debug",
            "KeySetting.xml");
#else
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "SettingsExtensions",
            "KeySetting.xml");
#endif

        #region Current

        private static KeySetting _current = new KeySetting();

        public static KeySetting Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        public void Load()
        {
            try
            {
                KeySettingListsss = filePath.ReadXml<List<Models.KeySetting>>();
            }
            catch (Exception)
            {
                KeySettingListsss = new List<Models.KeySetting>();
                KeySettingListsss.Add(new Models.KeySetting
                {
                    ModulesKey = Modules.PublicModulesKeys.GetModulesKey(Modules.PublicModulesKeys.ChangeTabs),
                    Key = Key.Tab,
                    ModifierKeys = ModifierKeys.Control,
                    Type = Enums.KeyType.Normal,
                });
                KeySettingListsss.Add(new Models.KeySetting
                {
                    ModulesKey = Modules.PublicModulesKeys.GetModulesKey(Modules.PublicModulesKeys.EnableSimpleFleet),
                    Key = Key.M,
                    ModifierKeys = ModifierKeys.Control,
                    Type = Enums.KeyType.Normal,
                });
                //Hotkey
                KeySettingListsss.Add(new Models.KeySetting
                {
                    ModulesKey = Modules.PublicModulesKeys.GetModulesKey(Modules.PublicModulesKeys.ChangeAllWindowsByMainWindow),
                    Key = Key.Tab,
                    ModifierKeys = ModifierKeys.Control | ModifierKeys.Shift,
                    Type = Enums.KeyType.HotKey,
                });
            }
            KeySettingList = new Dictionary<string, Models.KeySetting>();
            KeySettingListsss.ForEach(item => KeySettingList.Add(item.ModulesKey, item));
        }


        #endregion

        #region member
        public List<Models.KeySetting> KeySettingListsss { get; set; }

        [XmlIgnore]
        public Dictionary<string, Models.KeySetting> KeySettingList { get; set; }

        #endregion

        public Models.KeySetting Get(string key)
        {
            if (this.KeySettingList.ContainsKey(key))
            {
                return this.KeySettingList[key];
            }

            return null;
        }
        public void AddOrUpdate(Models.KeySetting keysetting)
        {
            var key = keysetting.ModulesKey;
            if (this.KeySettingList.ContainsKey(key))
            {
                this.KeySettingList[key] = keysetting;
            }
            else
            {
                this.KeySettingList.Add(key, keysetting);
            }
        }
        public bool Remove(Models.KeySetting keysetting)
        {
            return this.KeySettingList.Remove(keysetting.ModulesKey);
        }

        public void Save()
        {
            try
            {
                this.KeySettingListsss.Clear();
                foreach (var item in KeySettingList)
                {
                    this.KeySettingListsss.Add(item.Value);
                }

                this.KeySettingListsss.WriteXml(filePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
