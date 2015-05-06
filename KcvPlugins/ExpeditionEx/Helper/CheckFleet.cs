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
                }
            };




            return result;
        }
    }
}
