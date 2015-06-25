using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Models.Data.Xml;

namespace AMing.Plugins.Base.Generic
{
    /// <summary>
    /// 配置的基类 包含了一些基本操作继承之后可以直接使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SettingBase<T> : Interface.ISettings
        where T : new()
    {
        /// <summary>
        /// key
        /// </summary>
        public abstract string SettingKey { get; }
        /// <summary>
        /// 配置的文件名
        /// </summary>
        public abstract string SettingFileName { get; }
        /// <summary>
        /// 配置的目录名（可以包含目录不能重复否则只有一个生效）
        /// </summary>
        public abstract string SettingDirName { get; }
        /// <summary>
        /// 配置的文件路径
        /// </summary>
        public virtual string SettingFilePath
        {
            get
            {
                return Generic.SettingsHelper.SetSettingFilePath(this.SettingDirName, this.SettingFileName);
            }
            set { }
        }

        /// <summary>
        /// 配置是否已经初始化
        /// </summary>
        public bool IsInitialization { get; set; }

        /// <summary>
        /// 配置的数据
        /// </summary>
        public virtual T Settings { get; protected set; }

        /// <summary>
        /// 加载配置的方法（传null使用默认方法加载）
        /// </summary>
        public virtual Action LoadFunc
        {
            get
            {
                return () =>
                {
                    this.Settings = this.SettingFilePath.ReadXml<T>();
                };
            }
        }

        /// <summary>
        /// 保存配置的方法（传null使用默认方法保存）
        /// </summary>
        public virtual Action SaveFunc
        {
            get
            {
                return () =>
                {
                    this.Settings.WriteXml(this.SettingFilePath);
                };
            }
        }

        /// <summary>
        /// 将配置还原为默认值
        /// </summary>
        /// <returns></returns>
        public abstract void SetDefault();
    }
}
