using AMing.ViewRange.Enums;
using Grabacr07.KanColleViewer.ViewModels.Contents.Fleets;
using Grabacr07.KanColleViewer.Views.Controls;
using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMing.ViewRange.Extensions
{
    public static class FleetEx
    {
        /// <summary>
        /// 艦隊の索敵値を計算します。
        /// </summary>
        /// <returns></returns>
        public static int CalcFleetViewRange(this FleetViewModel fleet, ViewRangeType type)
        {
            if (fleet == null || fleet.Ships.Length == 0) return 0;

            var ships = fleet.Ships.Select(x => x.Ship);

            switch (type)
            {
                case ViewRangeType.Type1:
                    #region Type1

                    return ships.Sum(x => x.ViewRange);

                    #endregion
                //case ViewRangeType.Type2:
                //    #region Type2

                //    // http://wikiwiki.jp/kancolle/?%C6%EE%C0%BE%BD%F4%C5%E7%B3%A4%B0%E8#area5
                //    // [索敵装備と装備例] によって示されている計算式
                //    // stype=7 が偵察機 (2 倍する索敵値)、stype=8 が電探

                //    var spotter = ships.SelectMany(
                //        x => x.SlotItems
                //            .Zip(x.OnSlot, (i, o) => new { Item = i.Info, Slot = o })
                //            .Where(a => a.Item.RawData.api_type.Get(1) == 7)
                //            .Where(a => a.Slot > 0)
                //            .Select(a => a.Item.RawData.api_saku)
                //        ).Sum();

                //    var radar = ships.SelectMany(
                //        x => x.SlotItems
                //            .Where(i => i.Info.RawData.api_type.Get(1) == 8)
                //            .Select(i => i.Info.RawData.api_saku)
                //        ).Sum();

                //    return (spotter * 2) + radar + (int)Math.Sqrt(ships.Sum(x => x.ViewRange) - spotter - radar);

                //    #endregion
            }

            return 0;
        }
    }
}
