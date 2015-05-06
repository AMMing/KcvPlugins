using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Model
{
    public class ExpeditionShipTypes
    {
        /// <summary>
        /// 舰队类型id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 短名
        /// </summary>
        public string sname { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string abb { get; set; }
        /// <summary>
        /// 需要数量
        /// </summary>
        public string count { get; set; }
        /// <summary>
        /// 条件（如果数值一样则只需
        /// </summary>要满足一条即可）
        public string cond { get; set; }
        /// <summary>
        /// 禁止（不能使用该类型）
        /// </summary>
        public string ban { get; set; }
    }
}
