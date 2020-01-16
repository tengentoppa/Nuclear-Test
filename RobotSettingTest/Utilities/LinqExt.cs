using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSettingTest.Utilities
{
    public static class LinqExt
    {
        //REF: https://stackoverflow.com/a/20975582/288936
        /// <summary>
        /// Find all sub node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="childSelector"></param>
        /// <returns></returns>
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> childSelector)
        {
            var queue = new Queue<T>(items);
            while (queue.Any())
            {
                var next = queue.Dequeue();
                yield return next;
                foreach (var child in childSelector(next))
                    queue.Enqueue(child);
            }
        }

        public static string AggregateByNewLine(this IEnumerable<string> items)
        {
            return items.Aggregate((a, b) => $"{a}\n{b}");
        }
    }
}
