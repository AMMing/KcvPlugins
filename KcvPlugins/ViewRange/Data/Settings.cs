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

namespace AMing.ViewRange.Data
{
    [Serializable]
    public class Settings
    {
        #region static members

#if DEBUG
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "ViewRange.Debug",
            "Settings.xml");
#else
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "ViewRange",
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
                Type = Enums.ViewRangeType.Type3
            };
        }

        #endregion

        #region member

        public Enums.ViewRangeType Type { get; set; }

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
