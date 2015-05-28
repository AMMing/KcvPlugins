using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Base.Extensions;
using Grabacr07.KanColleViewer.Models.Data.Xml;
using System.IO;

namespace AMing.Plugins.Base.Generic
{
    public class SettingsHelper
    {
        #region current

        private static readonly SettingsHelper _current = new SettingsHelper();

        public static SettingsHelper Current
        {
            get { return _current; }
        }

        #endregion

        #region member

        /// <summary>
        /// 全部的配置
        /// </summary>
        public Dictionary<string, Interface.ISettings> Settings { get; private set; }

        #endregion

        #region method

        public SettingsHelper()
        {
            this.Settings = new Dictionary<string, Interface.ISettings>();
        }
        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool AddSetting(Interface.ISettings setting)
        {
            if (this.Settings.ContainsKey(setting.SettingKey)) return false;

            this.Settings.Add(setting.SettingKey, setting);

            return true;
        }
        /// <summary>
        /// 添加多个配置
        /// </summary>
        /// <param name="settings"></param>
        internal void AddSettings(IEnumerable<Interface.ISettings> settings)
        {
            if (settings == null) return;

            settings.ForEach(x =>
            {
                if (this.AddSetting(x))
                {
                    InitSetting(x);
                }
            });
        }
        /// <summary>
        /// 保存全部配置
        /// </summary>
        public void SaveAll()
        {
            this.Settings.ForEach(x => x.Value.SaveFunc());
        }

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string SetSettingFilePath(string dirName, string fileName)
        {
            if (string.IsNullOrWhiteSpace(dirName) || string.IsNullOrWhiteSpace(fileName)) return null;

            return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "kcvp.logs.moe",
                    dirName,
                    fileName);
        }
        /// <summary>
        /// 设置配置文件路径
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool SetSettingFilePath(Interface.ISettings setting)
        {
            setting.SettingFilePath = SetSettingFilePath(setting.SettingDirName, setting.SettingFileName);

            return !string.IsNullOrWhiteSpace(setting.SettingFilePath);
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool LoadSetting(Interface.ISettings setting)
        {
            if (string.IsNullOrWhiteSpace(setting.SettingFilePath) || setting.LoadFunc == null) return false;

            try
            {
                setting.LoadFunc();
            }
            catch (Exception ex)
            {
                setting.SetDefault();
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return true;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool SaveSetting(Interface.ISettings setting)
        {
            if (string.IsNullOrWhiteSpace(setting.SettingFilePath) || setting.SaveFunc == null) return false;

            try
            {
                setting.SaveFunc();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return true;
        }
        /// <summary>
        /// 初始化配置文件
        /// </summary>
        /// <param name="setting"></param>
        public static bool InitSetting(Interface.ISettings setting)
        {
            if (setting == null) return false;

            if (string.IsNullOrWhiteSpace(setting.SettingFilePath))
            {
                if (!SetSettingFilePath(setting)) return false;
            }
            return LoadSetting(setting);
        }

        #endregion
    }
}
