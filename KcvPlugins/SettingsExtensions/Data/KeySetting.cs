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

        public static KeySetting Current { get; set; }

        public static void Load()
        {
            try
            {
                Current = filePath.ReadXml<KeySetting>();
            }
            catch (Exception ex)
            {
                Current = GetInitialSettings();
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static KeySetting GetInitialSettings()
        {
            var list = new List<Models.KeySetting>();
            return new KeySetting
            {
                KeySettingList = list
            };
        }

        #endregion

        #region member

        public List<Models.KeySetting> KeySettingList { get; set; }

        #endregion

        private bool Exist(Models.KeySetting keysetting)
        {
            var result = false;
            this.KeySettingList.ForEach(item =>
            {
                if (item.ModifierKeys == keysetting.ModifierKeys || item.Key == keysetting.Key)
                {
                    result = true;
                }
            });

            return result;
        }

        private bool Add(Models.KeySetting keysetting)
        {
            if (Exist(keysetting))
            {
                return false;
            }

            this.KeySettingList.Add(keysetting);

            return true;
        }
        private bool Remove(Models.KeySetting keysetting)
        {
            return this.KeySettingList.Remove(keysetting);
        }

        public void Save()
        {
            try
            {
                this.WriteXml(filePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
