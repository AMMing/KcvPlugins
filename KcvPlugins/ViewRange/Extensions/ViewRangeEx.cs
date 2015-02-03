using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Core.Extensions;
using Grabacr07.KanColleViewer.ViewModels.Contents.Fleets;

namespace AMing.ViewRange.Extensions
{
    public static class ViewRangeEx
    {
        public static Fleet GetFleet(this FleetViewModel data)
        {
            return data.GetField<Fleet>("source");
        }
        public static T GetRawData<T>(this RawDataWrapper<T> data)
        {
            return data.GetField<T>("RawData");
        }
        public static int? Get(this int[] array, int index)
        {
            return array.Length > index ? (int?)array[index] : null;
        }
    }
}
