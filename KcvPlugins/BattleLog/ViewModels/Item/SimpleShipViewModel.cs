using AMing.Logger.Modes;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.ViewModels.Item
{
    public class SimpleShipViewModel : ViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public int LevelUpCount { get; set; }
        public int HP_Before { get; set; }
        public int HP_After { get; set; }
        public int HP_Max { get; set; }

        public SimpleShipViewModel(SimpleShip ship, int lvup)
        {
            this.Id = ship.Id;
            this.Name = ship.Name;
            this.Level = ship.Level;
            this.HP_Before = ship.HP_Before;
            this.HP_After = ship.HP_After;
            this.HP_Max = ship.HP_Max;
            this.LevelUpCount = lvup;
        }

    }
}
