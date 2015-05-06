using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Model
{
    public class ExpeditionInfoSimple
    {
        /// <summary>
        /// id
        /// </summary>summary>
        public int id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 难度
        /// </summary>
        public string r { get; set; }
        /// <summary>
        /// 海域
        /// </summary>
        public int a_id { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string des { get; set; }
        /// <summary>
        /// 所需时间
        /// </summary>
        public int time { get; set; }
        /// <summary>
        /// 消耗的燃料
        /// </summary>
        public int c_fuel { get; set; }
        /// <summary>
        /// 消耗的弹药
        /// </summary>
        public int c_ammun { get; set; }
        /// <summary>
        /// 获取的基本燃料
        /// </summary>
        public int g_fuel { get; set; }
        /// <summary>
        /// 获取的基本弹药
        /// </summary>
        public int g_ammun { get; set; }
        /// <summary>
        /// 获取的基本钢
        /// </summary>
        public int g_steel { get; set; }
        /// <summary>
        /// 获取的基本铝
        /// </summary>
        public int g_alumi { get; set; }
        /// <summary>
        /// 获取的基本经验值
        /// </summary>
        public int g_exp { get; set; }
        /// <summary>
        /// 获取的资材
        /// </summary>
        public string g_bm { get; set; }
        /// <summary>
        /// 获取的高速建造
        /// </summary>
        public string g_hsb { get; set; }
        /// <summary>
        /// 获取的高速修复
        /// </summary>
        public string g_hsr { get; set; }
        /// <summary>
        /// 获取的家具箱（小）
        /// </summary>
        public string g_f_s { get; set; }
        /// <summary>
        /// 获取的家具箱（中）
        /// </summary>
        public string g_f_m { get; set; }
        /// <summary>
        /// 获取的家具箱（大）
        /// </summary>
        public string g_f_b { get; set; }
        /// <summary>
        /// 等级总和
        /// </summary>
        public int sum_lv { get; set; }
        /// <summary>
        /// 旗舰等级
        /// </summary>
        public int fs_lv { get; set; }
        /// <summary>
        /// 旗舰类型
        /// </summary>
        public int fs_t { get; set; }
        /// <summary>
        /// 需要多少艘船
        /// </summary>
        public int s_count { get; set; }
        /// <summary>
        /// 需要带缶的数量
        /// </summary>
        public int b_count { get; set; }
        /// <summary>
        /// 需要带缶船的数量
        /// </summary>
        public int bs_count { get; set; }
        /// <summary>
        /// 说明（为空自动拼接数据说明）
        /// </summary>
        public string expl { get; set; }
        /// <summary>
        /// 追加说明
        /// </summary>
        public string aexpl { get; set; }
        /// <summary>
        /// 舰种详细
        /// </summary>
        public List<Model.ExpeditionShipTypes> s_types { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string c_date { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string u_date { get; set; }
    }
}
