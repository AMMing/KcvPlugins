using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AMing.Plugins.Base.Interface
{
    /// <summary>
    /// 类似kcv的插件，但是不会被kcv的插件机制调用，而是被Base模块调用并自动完成相关数据方法的调用
    /// </summary>
    public interface IPlugin : IPluginBase, IDisposable
    {
        /// <summary>
        /// 分类
        /// </summary>
        Enums.PluginCategory Category { get; }
        /// <summary>
        /// 初始化开始
        /// </summary>
        void Initialize_Start();
        /// <summary>
        /// 初始化结束
        /// </summary>
        void Initialize_End();
        /// <summary>
        /// 按钮图案
        /// </summary>
        /// <returns></returns>
        ImageSource ItemButton { get; }
        /// <summary>
        /// 设置界面
        /// </summary>
        /// <returns></returns>
        object SettingsView();
        /// <summary>
        /// 设置获取设置对象（自动进行加载及保存）
        /// </summary>
        IEnumerable<ISettings> Settings { get; }
        /// <summary>
        /// 获取全部模块（自动初始化及注销）
        /// </summary>
        IEnumerable<IModules> Modules { get; }
    }
}
