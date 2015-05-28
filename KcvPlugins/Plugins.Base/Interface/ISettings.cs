using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Interface
{
    public interface ISettings
    {
        /// <summary>
        /// key
        /// </summary>
        string SettingKey { get; }
        /// <summary>
        /// 配置的文件名
        /// </summary>
        string SettingFileName { get; }
        /// <summary>
        /// 配置的目录名（可以包含目录不能重复否则只有一个生效）
        /// </summary>
        string SettingDirName { get; }
        /// <summary>
        /// 配置的文件路径
        /// </summary>
        string SettingFilePath { get; set; }

        /// <summary>
        /// 加载配置的方法（传null使用默认方法加载）
        /// </summary>
        Action LoadFunc { get; }

        /// <summary>
        /// 保存配置的方法（传null使用默认方法保存）
        /// </summary>
        Action SaveFunc { get; }

        /// <summary>
        /// 将配置还原为默认值
        /// </summary>
        /// <returns></returns>
        void SetDefault();
    }
}
