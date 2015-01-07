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

namespace AMing.SettingsExtensions.Data
{
    [Serializable]
    public class Settings
    {
        #region static members

        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "SettingsExtensions",
            "Settings.xml");

        public static Settings Current { get; set; }

        public static void Load()
        {
            try
            {
                Current = filePath.ReadXml<Settings>();
            }
            catch (Exception ex)
            {
                Current = GetInitialSettings();
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static Settings GetInitialSettings()
        {
            return new Settings
            {
                EnableExitTip = true,
                EnableNotifyIcon = true,
                EnableWindowMiniHideTaskbar = true,
                NotifyIcon_Path = "pack://application:,,,/KanColleViewer;Component/Assets/app.ico"
            };
        }

        #endregion

        #region prop

        public bool EnableExitTip { get; set; }

        public bool EnableNotifyIcon { get; set; }

        public bool EnableWindowMiniHideTaskbar { get; set; }


        public string NotifyIcon_Path { get; set; }


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
