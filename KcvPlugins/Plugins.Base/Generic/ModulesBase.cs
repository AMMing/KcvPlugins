using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Generic
{
    /// <summary>
    /// 模块的基类 包含了一些基本操作继承之后可以直接使用
    /// </summary>
    public abstract class ModulesBase : Interface.IModules
    {
        /// <summary>
        /// key
        /// </summary>
        public abstract string Key { get; set; }
        /// <summary>
        /// 模块是否已经初始化
        /// </summary>
        public virtual bool IsInitialization { get; set; }
        /// <summary>
        /// 初始化开始
        /// </summary>
        public virtual void Initialize_Start() { }
        /// <summary>
        /// 初始化结束
        /// </summary>
        public virtual void Initialize_End() { }
        /// <summary>
        /// 释放模块
        /// </summary>
        public virtual void Dispose() { }
    }
}
