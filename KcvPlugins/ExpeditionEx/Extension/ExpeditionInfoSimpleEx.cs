using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Extension
{
    public static class ModelEx
    {
        public static Model.ExpeditionInfo ToModel(this Model.ExpeditionInfoSimple m)
        {
            if (m == null) return null;

            return new Model.ExpeditionInfo
            {
                Id = m.id,
                Number = m.num,
                Name = m.name,
                Rank = m.r,
                AreaId = m.a_id,
                Description = m.des,
                Time = m.time,
                ConsumeFuel = m.c_fuel,
                ConsumeAmmunition = m.c_ammun,
                GetFuel = m.g_fuel,
                GetAmmunition = m.g_ammun,
                GetSteel = m.g_steel,
                GetAluminum = m.g_alumi,
                GetExp = m.g_exp,
                GetBuildingMaterials = m.g_bm,
                GetHighSpeedBuilding = m.g_hsb,
                GetHighSpeedRepair = m.g_hsr,
                GetFurnitureSmall = m.g_f_s,
                GetFurnitureModerate = m.g_f_m,
                GetFurnitureBig = m.g_f_b,
                SumLevel = m.sum_lv,
                FlagshipLevel = m.fs_lv,
                FlagshipType = m.fs_t,
                ShipCount = m.s_count,
                BarrelCount = m.b_count,
                BarrelShipCount = m.bs_count,
                Explanation = m.expl,
                AppendExplanation = m.aexpl,
                ShipTypes = m.s_types,
                CreateDate = m.c_date,
                UpdateDate = m.u_date
            };
        }
        public static Model.ExpeditionInfoSimple ToSimple(this Model.ExpeditionInfo m)
        {
            if (m == null) return null;

            return new Model.ExpeditionInfoSimple
            {
                id = m.Id,
                num = m.Number,
                name = m.Name,
                r = m.Rank,
                a_id = m.AreaId,
                des = m.Description,
                time = m.Time,
                c_fuel = m.ConsumeFuel,
                c_ammun = m.ConsumeAmmunition,
                g_fuel = m.GetFuel,
                g_ammun = m.GetAmmunition,
                g_steel = m.GetSteel,
                g_alumi = m.GetAluminum,
                g_exp = m.GetExp,
                g_bm = m.GetBuildingMaterials,
                g_hsb = m.GetHighSpeedBuilding,
                g_hsr = m.GetHighSpeedRepair,
                g_f_s = m.GetFurnitureSmall,
                g_f_m = m.GetFurnitureModerate,
                g_f_b = m.GetFurnitureBig,
                sum_lv = m.SumLevel,
                fs_lv = m.FlagshipLevel,
                fs_t = m.FlagshipType,
                s_count = m.ShipCount,
                b_count = m.BarrelCount,
                bs_count = m.BarrelShipCount,
                expl = m.Explanation,
                aexpl = m.AppendExplanation,
                s_types = m.ShipTypes,
                c_date = m.CreateDate,
                u_date = m.UpdateDate
            };
        }
    }
}
