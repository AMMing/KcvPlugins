﻿using System;
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
    public class Settings
    {
        #region static members

#if DEBUG
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "SettingsExtensions.Debug",
            "Settings.xml");
#else
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "SettingsExtensions",
            "Settings.xml");
#endif

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
                NotifyIcon_Path = DefaultNotifyIconPath,
#if DEBUG
                EnableHotKeyShowHide = true,
#else
                EnableHotKeyShowHide = false,
#endif
                HotKey_Key = Key.Tab,
                HotKey_ModifierKeys = ModifierKeys.Shift | ModifierKeys.Control,
                WindowTheme_Theme = Theme.Dark,
                WindowTheme_Accent = Accent.Blue
            };
        }

        #endregion

        #region member

        public bool EnableExitTip { get; set; }

        public bool EnableNotifyIcon { get; set; }

        public bool EnableWindowMiniHideTaskbar { get; set; }

        public const string DefaultNotifyIconPath = "pack://application:,,,/KanColleViewer;Component/Assets/app.ico";
        public string NotifyIcon_Path { get; set; }

        public bool EnableHotKeyShowHide { get; set; }
        public Key HotKey_Key { get; set; }
        public ModifierKeys HotKey_ModifierKeys { get; set; }

        public Theme WindowTheme_Theme { get; set; }
        public Accent WindowTheme_Accent { get; set; }

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
