using System;
using System.Collections.Generic;
using System.Linq;

namespace InternetShopParser.Model.Database.Extensions
{
    public static class EnumerableExtensions
    {
        public static string JoinStr(this IEnumerable<string> enumerable, string joiner = ", ")
        => JoinStr(enumerable, x => x, joiner);

        public static string JoinStr<T>(this IEnumerable<T> enumerable, Func<T, string> selector, string joiner = ", ")
        => string.Join(joiner, enumerable.Select(selector));

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }
    }
}
