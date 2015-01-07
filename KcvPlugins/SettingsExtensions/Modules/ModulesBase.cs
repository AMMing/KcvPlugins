using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.Modules
{
    public class ModulesBase : IDisposable
    {
        bool isinit = false;
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Initialize()
        {
            if (isinit)
            {
                return;
            }
            isinit = true;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose() { }
    }
}
