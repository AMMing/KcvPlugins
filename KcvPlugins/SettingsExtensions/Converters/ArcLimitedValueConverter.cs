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

namespace AMing.SettingsExtensions.Converters
{
    public class ArcLimitedValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int arc_max = 0;
            int.TryParse(parameter.ToString(), out arc_max);
            if (value is LimitedValue)
            {
                var limitedValue = (LimitedValue)value;
                var percentage = limitedValue.Percentage();
                var result = percentage * Math.Abs(arc_max);
                if (arc_max < 0)
                {
                    result = Math.Abs(arc_max) - result;
                }
                return result;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
