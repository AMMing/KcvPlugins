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

        public int HP_Before { get; set; }
        public int HP_After { get; set; }
        public int HP_Max { get; set; }


        public SimpleShip() { }

        public SimpleShip(kcsapi_battleresult_getship ship)
        {
            this.Id = ship.api_ship_id;
            this.Name = ship.api_ship_name;
            this.Level = 1;
        }
        public SimpleShip(Api_Get_Ship ship)
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
            this.HP_Before = ship.HP.Current;
            this.HP_Max = ship.HP.Maximum;
        }

        /// <summary>
        /// 是否是战斗之后的HP
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public bool IsAfterHP(Ship ship)
        {
            return ship.Id == this.Id && this.HP_After == 0 && ship.HP.Current <= this.HP_Before;
        }

        /// <summary>
        /// 设置战斗之后的HP
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public bool SetAfterHP(Ship ship)
        {
            if (IsAfterHP(ship))
            {
                this.HP_After = ship.HP.Current;

                return true;
            }

            return false;
        }
    }
}
