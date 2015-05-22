using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Extensions
{
    public static class LimitedValueEx
    {
        public static double Percentage(this LimitedValue limitedValue)
        {
            return limitedValue.Maximum == 0 ? 0.0 : limitedValue.Current / (double)limitedValue.Maximum;
        }

        public static Enums.ShipStatus ShipStatus(this LimitedValue limitedValue)
        {
            var percentage = limitedValue.Percentage();

            if (percentage <= 0.25)
                return Enums.ShipStatus.SevereDamage;
            else if (percentage <= 0.5)
                return Enums.ShipStatus.ModerateDamage;
            else if (percentage <= 0.75)
                return Enums.ShipStatus.MinorDamage;
            else
                return Enums.ShipStatus.Normal;
        }
    }
}
