using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Grabacr07.KanColleViewer.ViewModels.Contents.Fleets;

namespace AMing.SettingsExtensions.Converters
{
    public class FleetsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FleetViewModel[])
            {
                var feets = value as FleetViewModel[];
                if (feets != null && feets.Count() > 0)
                {
                    return feets.FirstOrDefault();
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
