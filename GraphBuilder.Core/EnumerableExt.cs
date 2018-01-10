using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphBuilder.Core
{
    public static class EnumerableExt
    {
        /// <summary>
        /// Usage: Item item = items.PickRandom();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">IEnumerable</param>
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }


        private static Random rng = new Random();
        /// <summary>
        /// Fisher–Yates shuffle
        /// Usage: Item item = items.Shuffle();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        } 
    }
}