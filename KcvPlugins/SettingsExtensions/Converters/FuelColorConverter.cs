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
using AMing.Plugins.Core.Enums;

namespace AMing.SettingsExtensions.Converters
{
    public class FuelColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LimitedValue)
            {
                var limitedValue = (LimitedValue)value;

                Color color;

                switch (limitedValue.ShipStatus())
                {
                    case ShipStatus.Normal:
                        color = Color.FromRgb(135, 98, 201);
                        break;
                    case ShipStatus.MinorDamage:
                        color = Color.FromRgb(190, 160, 243);
                        break;
                    case ShipStatus.ModerateDamage:
                        color = Color.FromRgb(252, 127, 213);
                        break;
                    default:
                        color = Color.FromRgb(255, 88, 88);
                        break;
                }

                return new SolidColorBrush(color);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
