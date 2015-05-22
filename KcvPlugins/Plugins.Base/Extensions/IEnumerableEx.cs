using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Plugins.Base.Extensions
{
    public static class IEnumerableEx
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence) action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T, int> action)
        {
            int index = 0;
            foreach (var item in sequence)
            {
                action(item, index);
                index++;
            }
        }
    }
}
