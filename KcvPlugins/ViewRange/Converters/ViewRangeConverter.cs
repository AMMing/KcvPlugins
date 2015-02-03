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
using Grabacr07.KanColleWrapper;
using AMing.ViewRange.Extensions;

namespace AMing.ViewRange.Converters
{
    public class ViewRangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as FleetViewModel;
            AMing.Plugins.Core.GenericMessager.Current.Send(Plugins.Core.Enums.MessageType.Logs, data);
            if (data != null)
            {
                var val = data.CalcFleetViewRange(Enums.ViewRangeType.Type1);
                return string.Format("val_{0}", val);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
