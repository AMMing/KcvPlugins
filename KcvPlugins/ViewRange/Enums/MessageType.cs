using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ViewRange.Enums
{
    public enum ViewRangeType
    {
        /// <summary>
        /// 単純な索敵合計値。
        /// </summary>
        Type1,

        /// <summary>
        /// (偵察機 × 2) + (電探) + √(装備込みの艦隊索敵値合計 - 偵察機 - 電探)。
        /// </summary>
        Type2,


        Type3,


        Type4
    }

}
