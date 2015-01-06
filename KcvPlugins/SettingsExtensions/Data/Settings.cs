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
    public class Settings : NotificationObject
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
                Enable_ExitTip = true,
                Enable_NotifyIcon = true
            };
        }

        #endregion



        private bool _enable_ExitTip;

        public bool Enable_ExitTip
        {
            get { return _enable_ExitTip; }
            set
            {
                if (this._enable_ExitTip != value)
                {
                    this._enable_ExitTip = value;
                    this.RaisePropertyChanged();
                }
            }
        }


        private bool _enable_NotifyIcon;

        public bool Enable_NotifyIcon
        {
            get { return _enable_NotifyIcon; }
            set
            {
                if (this._enable_NotifyIcon != value)
                {
                    this._enable_NotifyIcon = value;
                    this.RaisePropertyChanged();
                }
            }
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
