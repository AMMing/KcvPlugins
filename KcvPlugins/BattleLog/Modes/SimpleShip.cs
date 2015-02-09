using Grabacr07.KanColleWrapper.Models;
using Grabacr07.KanColleWrapper.Models.Raw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public class SimpleShip
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public int HP_Current { get; set; }
        public int HP_Max { get; set; }


        public SimpleShip() { }

        public SimpleShip(kcsapi_battleresult_getship ship)
        {
            this.Id = ship.api_ship_id;
            this.Name = ship.api_ship_name;
            this.Level = 1;
        }
        public SimpleShip(Ship ship)
        {
            this.Id = ship.Id;
            this.Name = ship.Info.Name;
            this.Level = ship.Level;
            this.HP_Current = ship.HP.Current;
            this.HP_Max = ship.HP.Maximum;
        }
    }
}
