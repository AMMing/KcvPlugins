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

namespace AMing.Warning.Data
{
    [Serializable]
    public class Settings
    {
        #region static members

#if DEBUG
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "Warning.Debug",
            "Settings.xml");
#else
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "y2443.com",
            "Warning",
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
                EnableWarning = true,
                EnableThemeWarning = true,
                EnableWindows = true,
                EnableFleet1 = true,
                EnableFleet2 = true,
                EnableFleet3 = false,
                EnableFleet4 = false,
                FilterInRepairing = true,
                EnableWarningEx = true,
            };
        }

        #endregion

        #region member

        /// <summary>
        /// 启用大破警告
        /// </summary>
        public bool EnableWarning { get; set; }


        /// <summary>
        /// 启用警告主题（kcv主窗体红色主题）
        /// </summary>
        public bool EnableThemeWarning { get; set; }
        /// <summary>
        /// 启用右下角大破警告窗体
        /// </summary>
        public bool EnableWindows { get; set; }
        /// <summary>
        /// 监听 第一舰队
        /// </summary>
        public bool EnableFleet1 { get; set; }
        /// <summary>
        /// 监听 第二舰队
        /// </summary>
        public bool EnableFleet2 { get; set; }
        /// <summary>
        /// 监听 第三舰队
        /// </summary>
        public bool EnableFleet3 { get; set; }
        /// <summary>
        /// 监听 第四舰队
        /// </summary>
        public bool EnableFleet4 { get; set; }

        /// <summary>
        /// 忽略入渠中的大破舰娘
        /// </summary>
        public bool FilterInRepairing { get; set; }

        /// <summary>
        /// 启用其他插件的警告通知
        /// </summary>
        public bool EnableWarningEx { get; set; }

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
