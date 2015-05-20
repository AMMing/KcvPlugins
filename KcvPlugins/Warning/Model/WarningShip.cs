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
        public int Id { get; set; }

        public int FleetIndex { get; set; }

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

        public override string ToString()
        {
            return string.Format("Id:{0}\tFleetIndex:{1}\tName:{2}\tHP:{3}/{4}\tSituation:{5}\t",
                this.Id,
                this.FleetIndex,
                this.Name,
                this.HP.Current, this.HP.Maximum,
                this.Situation
                );
        }
    }
}
