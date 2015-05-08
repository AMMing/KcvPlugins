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
                    AtLeast = expinfo.SumLevel,
                    Now = fleet.Ships.Sum(x => x.Level)
                },
                FlagShipLevel = new Model.Claim
                {
                    AtLeast = expinfo.FlagshipLevel
                },
                ShipCount = new Model.Claim
                {
                    AtLeast = expinfo.ShipCount,
                    Now = fleet.Ships.Count()
                },
                BarrelCount = new Model.Claim
                {
                    AtLeast = expinfo.BarrelCount
                },
                BarrelShipCount = new Model.Claim
                {
                    AtLeast = expinfo.BarrelShipCount
                },
                FleetShipType = new Model.GroupClaim(expinfo.FlagshipType),
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
            var flagship = new List<Ship>();
            flagship.Add(ships.FirstOrDefault());
            result.FleetShipType.Check(flagship);
            //result.ShipType.Check(ships);



            return result;
        }


    }
}