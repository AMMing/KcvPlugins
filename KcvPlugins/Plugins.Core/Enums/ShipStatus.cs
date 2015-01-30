using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Core.Enums
{
    /// <summary>
    /// 船体状态
    /// </summary>
    public enum ShipStatus
    {
        /// <summary>
        /// 正常(HP 75% 以上)
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 小破(HP 75% 到 50%)
        /// </summary>
        MinorDamage = 1,
        /// <summary>
        /// 中破(HP 50% 到 25%)
        /// </summary>
        ModerateDamage = 2,
        /// <summary>
        /// 大破(HP 25% 以下)
        /// </summary>
        SevereDamage = 3
    }
}
