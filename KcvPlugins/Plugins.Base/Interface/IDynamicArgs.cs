using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Interface
{
    /// <summary>
    /// 动态参数传递实体的接口
    /// </summary>
    public interface IDynamicArgs
    {
        /// <summary>
        /// 验证key
        /// </summary>
        string ValidationKey { get; }

        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Validation(string key);
    }
}
