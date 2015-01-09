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

namespace AMing.QuestsExtensions.Data
{
    [Serializable]
    public class Settings
    {
        #region static members

#if DEBUG
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "QuestsExtensions.Debug",
            "Settings.xml");
#else
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "QuestsExtensions",
            "Settings.xml");
#endif

        public static Settings Current { get; set; }

        public static bool IsLoad { get; set; }

        public static void FirstLoad()
        {
            if (IsLoad) return;
            Load();
        }

        public static void Load()
        {
            try
            {
                Current = filePath.ReadXml<Settings>();
                IsLoad = true;
            }
            catch (Exception ex)
            {
                Current = GetInitialSettings();
                System.Diagnostics.Debug.WriteLine(ex);
                IsLoad = false;
            }
        }

        public static Settings GetInitialSettings()
        {
            return new Settings
            {
            };
        }

        #endregion

        #region member

        public string CurrentShowLocalQuests { get; set; }

        #endregion

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
