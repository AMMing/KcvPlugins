using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Model
{
    public class ExpeditionInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 难度
        /// </summary>
        public string Rank { get; set; }
        /// <summary>
        /// 海域
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 所需时间
        /// </summary>
        public int Time { get; set; }
        /// <summary>
        /// 消耗的燃料
        /// </summary>
        public int ConsumeFuel { get; set; }
        /// <summary>
        /// 消耗的弹药
        /// </summary>
        public int ConsumeAmmunition { get; set; }
        /// <summary>
        /// 获取的基本燃料
        /// </summary>
        public int GetFuel { get; set; }
        /// <summary>
        /// 获取的基本弹药
        /// </summary>
        public int GetAmmunition { get; set; }
        /// <summary>
        /// 获取的基本钢
        /// </summary>
        public int GetSteel { get; set; }
        /// <summary>
        /// 获取的基本铝
        /// </summary>
        public int GetAluminum { get; set; }
        /// <summary>
        /// 获取的基本经验值
        /// </summary>
        public int GetExp { get; set; }
        /// <summary>
        /// 获取的资材
        /// </summary>
        public string GetBuildingMaterials { get; set; }
        /// <summary>
        /// 获取的高速建造
        /// </summary>
        public string GetHighSpeedBuilding { get; set; }
        /// <summary>
        /// 获取的高速修复
        /// </summary>
        public string GetHighSpeedRepair { get; set; }
        /// <summary>
        /// 获取的家具箱（小）
        /// </summary>
        public string GetFurnitureSmall { get; set; }
        /// <summary>
        /// 获取的家具箱（中）
        /// </summary>
        public string GetFurnitureModerate { get; set; }
        /// <summary>
        /// 获取的家具箱（大）
        /// </summary>
        public string GetFurnitureBig { get; set; }
        /// <summary>
        /// 等级总和
        /// </summary>
        public int SumLevel { get; set; }
        /// <summary>
        /// 旗舰等级
        /// </summary>
        public int FlagshipLevel { get; set; }
        /// <summary>
        /// 旗舰类型
        /// </summary>
        public int FlagshipType { get; set; }
        /// <summary>
        /// 需要多少艘船
        /// </summary>
        public int ShipCount { get; set; }
        /// <summary>
        /// 需要带缶的数量
        /// </summary>
        public int BarrelCount { get; set; }
        /// <summary>
        /// 需要带缶船的数量
        /// </summary>
        public int BarrelShipCount { get; set; }
        /// <summary>
        /// 说明（为空自动拼接数据说明）
        /// </summary>
        public string Explanation { get; set; }
        /// <summary>
        /// 追加说明
        /// </summary>
        public string AppendExplanation { get; set; }
        /// <summary>
        /// 舰种详细
        /// </summary>
        public List<ExpeditionShipTypes> ShipTypes { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdateDate { get; set; }
    }
}
