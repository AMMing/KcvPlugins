using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Extensions
{
    public static class EnumEx
    {
        public static void ForEach<T>(Action<T> action)
        {
            var type = typeof(T);
            Enum.GetNames(type).ToList().ForEach(item => action((T)Enum.Parse(type, item)));
        }
        public static void ForEach<T>(this Enum val, Action<T> action)
        {
            ForEach(action);
        }
    }
}
