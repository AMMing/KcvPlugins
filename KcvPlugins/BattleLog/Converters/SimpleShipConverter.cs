using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Grabacr07.KanColleViewer.ViewModels.Contents.Fleets;
using Grabacr07.KanColleWrapper.Models;
using AMing.Plugins.Core.Extensions;
using AMing.Logger.ViewModels.Item;

namespace AMing.Logger.Converters
{
    public class SimpleShipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SimpleShipViewModel)
            {
                var ship = value as SimpleShipViewModel;
                if (ship != null)
                {
                    return string.Format("[Id {0}] {1} [Lv.{2}{3}][HP {4}/{5}]",
                        ship.Id,
                        ship.Name,
                        ship.Level,
                        ship.LevelUpCount > 0 ?
                            string.Format("→{0}", ship.Level + ship.LevelUpCount) :
                            string.Empty,

                        ship.HP_After != ship.HP_Before ?
                            string.Format("{0}→{1}", ship.HP_Before, ship.HP_After) :
                            string.Format("{0}", ship.HP_After),
                        ship.HP_Max
                        );
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
