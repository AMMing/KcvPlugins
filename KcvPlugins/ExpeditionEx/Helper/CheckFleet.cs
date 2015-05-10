using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.ExpeditionEx.Helper
{
    public class CheckFleet
    {
        private static CheckFleet _current = new CheckFleet();

        public static CheckFleet Current
        {
            get { return _current; }
            set { _current = value; }
        }


        /// <summary>
        /// 缶 id 75
        /// </summary>
        const int barrelItemId = 75;
        public Model.CheckFleetResult Check(Fleet fleet, Model.ExpeditionInfo expinfo)
        {
            var result = new Model.CheckFleetResult
            {
                SumLevel = new Model.Claim
                {
                    ErrorMessageFormat = "舰队总等级（Lv{1}） 达不到远征的最低要求（Lv{0}）",
                    AtLeast = expinfo.SumLevel,
                    Now = fleet.Ships.Sum(x => x.Level)
                },
                FlagShipLevel = new Model.Claim
                {
                    ErrorMessageFormat = "旗舰等级（Lv{1}） 达不到远征的最低要求（Lv{0}）",
                    AtLeast = expinfo.FlagshipLevel
                },
                ShipCount = new Model.Claim
                {
                    ErrorMessageFormat = "舰数（{1}艘） 达不到远征的最低要求（{0}艘）",
                    AtLeast = expinfo.ShipCount,
                    Now = fleet.Ships.Count()
                },
                BarrelCount = new Model.Claim
                {
                    ErrorMessageFormat = "舰队所以装备的（运输用缶）（{1}个） 达不到远征的最低要求（{0}个）",
                    AtLeast = expinfo.BarrelCount
                },
                BarrelShipCount = new Model.Claim
                {
                    ErrorMessageFormat = "装备的（运输用缶）的舰数（{1}艘） 达不到远征的最低要求（{0}艘）",
                    AtLeast = expinfo.BarrelShipCount
                },
                FleetShipType = new Model.GroupClaim(expinfo.FlagshipType)
                {
                    ErrorMessageFormat = "旗舰舰种不是远征需要的类型[{0}]",
                },
                ShipType = new Model.ExpeditionShipTypesClaim(expinfo.ShipTypes),
                Claims = new List<Model.Claim>()
            };
            result.Claims.Add(result.SumLevel);
            result.Claims.Add(result.FlagShipLevel);
            result.Claims.Add(result.ShipCount);
            result.Claims.Add(result.ShipType);
            result.Claims.Add(result.FleetShipType);
            result.Claims.Add(result.BarrelCount);
            result.Claims.Add(result.BarrelShipCount);

            var barrelShips = fleet.Ships.Select(x => x.EquippedSlots.Count(i => i.Item.Id == barrelItemId));

            result.BarrelShipCount.Now = barrelShips.Count(x => x > 0);
            result.BarrelCount.Now = barrelShips.Sum();

            var ships = fleet.Ships;
            var flagship = ships.FirstOrDefault();
            var flagship_list = new List<Ship>();

            if (flagship != null)
            {
                flagship_list.Add(ships.FirstOrDefault());
                result.FleetShipType.Check(flagship_list);

                result.FlagShipLevel.Now = flagship.Level;
            }

            result.ShipType.Check(ships);

            return result;
        }


    }
}