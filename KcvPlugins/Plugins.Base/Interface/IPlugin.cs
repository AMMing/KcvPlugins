using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Interface
{
    /// <summary>
    /// 类似kcv的插件，但是不会被kcv的插件机制调用，而是被Base模块调用并自动完成相关数据方法的调用
    /// </summary>
    public interface IPlugin : IPluginBase
    {
        /// <summary>
        /// 初始化时调用
        /// </summary>
        void Init();

        /// <summary>
        /// 退出时调用
        /// </summary>
        void Exit();

        /// <summary>
        /// 设置界面
        /// </summary>
        /// <returns></returns>
        object SettingsView();

        /// <summary>
        /// 设置获取设置对象（自动进行加载及保存）
        /// </summary>
        IEnumerable<ISettings> Settings();

        /// <summary>
        /// 获取全部模块（自动初始化及注销）
        /// </summary>
        /// <returns></returns>
        IEnumerable<IModules> Modules();
    }
}
