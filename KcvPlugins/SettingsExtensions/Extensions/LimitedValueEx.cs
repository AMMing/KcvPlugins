using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.Extensions
{
    public static class LimitedValueEx
    {
        public static double Percentage(this LimitedValue limitedValue)
        {
            return limitedValue.Maximum == 0 ? 0.0 : limitedValue.Current / (double)limitedValue.Maximum;
        }
    }
}
