using AMing.Plugins.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AMing.Plugins.Base.Generic
{
    public abstract class PluginBase : Interface.IPlugin
    {

        /// <summary>
        /// 插件的key必须唯一
        /// </summary>
        public abstract string PluginKey { get; }
        /// <summary>
        /// 插件的名称
        /// </summary>
        public abstract string PluginName { get; }
        /// <summary>
        /// 分类
        /// </summary>
        public virtual Enums.PluginCategory Category { get { return Enums.PluginCategory.Common; } }
        /// <summary>
        /// 插件描述
        /// </summary>
        public virtual string Description { get { return null; } }
        /// <summary>
        /// 插件的版本
        /// </summary>
        public abstract Version PluginVersion { get; }
        /// <summary>
        /// 初始化开始
        /// </summary>
        public virtual void Initialize_Start()
        {
            InitSettings();
            InitModules();
        }
        /// <summary>
        /// 初始化结束
        /// </summary>
        public virtual void Initialize_End() { }
        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose() { }
        /// <summary>
        /// 按钮图案
        /// </summary>
        /// <returns></returns>
        public abstract ImageSource ItemButton { get; }
        /// <summary>
        /// 设置界面
        /// </summary>
        /// <returns></returns>
        public abstract object SettingsView();


        protected List<ISettings> _settings = null;
        /// <summary>
        /// 配置对象（自动进行加载及保存）
        /// </summary>
        public virtual IEnumerable<ISettings> Settings
        {
            get { return _settings; }
        }

        protected List<IModules> _modules = null;
        /// <summary>
        /// 全部模块（自动初始化及注销）
        /// </summary>
        public virtual IEnumerable<IModules> Modules
        {
            get { return _modules; }
        }

        /// <summary>
        /// 初始化配置列表
        /// </summary>
        public virtual void InitSettings()
        {
            this._settings = new List<ISettings>();
        }
        /// <summary>
        /// 初始化模块列表
        /// </summary>
        public virtual void InitModules()
        {
            this._modules = new List<IModules>();
        }

    }
}
