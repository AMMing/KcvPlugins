using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Interface
{
    /// <summary>
    /// 用于Base中查询是否注册了相关的插件
    /// </summary>
    public interface IPluginBase
    {
        /// <summary>
        /// 插件的key必须唯一
        /// </summary>
        string PluginKey { get; }
        /// <summary>
        /// 插件的名称
        /// </summary>
        string PluginName { get; }
        /// <summary>
        /// 插件的版本
        /// </summary>
        Version PluginVersion { get; }
        /// <summary>
        /// 插件描述
        /// </summary>
        string Description { get; }
    }
}
