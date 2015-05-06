using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Model
{
    public class CheckFleetResult
    {
        /// <summary>
        /// 总等级
        /// </summary>
        public Claim SumLevel { get; set; }

        /// <summary>
        /// 旗舰等级
        /// </summary>
        public Claim FlagShipLevel { get; set; }

        /// <summary>
        /// 需要的船数量
        /// </summary>
        public Claim ShipCount { get; set; }

        /// <summary>
        /// 舰队类型
        /// </summary>
        public ShipTypeClaim ShipType { get; set; }

        /// <summary>
        /// 旗舰类型
        /// </summary>
        public ShipTypeClaim FleetShipType { get; set; }

        /// <summary>
        /// 缶的总数
        /// </summary>
        public Claim BarrelCount { get; set; }

        /// <summary>
        /// 带缶船总数
        /// </summary>
        public Claim BarrelShipCount { get; set; }
    }
}
