using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phone
{
    static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable) action(item);
        }
    }
}
