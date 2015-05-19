using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Warning.Model
{
    public class WarningShip
    {
        public int FleetIndex { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }

        public LimitedValue HP { get; set; }

        public ShipSituation Situation { get; set; }

        public WarningShip(Ship ship, int fleet_index)
        {
            this.FleetIndex = fleet_index;
            this.Id = ship.Id;
            this.Name = ship.Info.Name;
            this.HP = ship.HP;
            this.Situation = ship.Situation;
        }
    }
}
