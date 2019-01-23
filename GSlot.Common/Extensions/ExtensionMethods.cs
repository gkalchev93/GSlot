using System;
using System.Collections.Generic;

namespace GSlot.Common
{
    public static class ExtensionMethods
    {
        private static Random rng = new Random();

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

        public static T GetRandomItem<T>(this IList<T> list)
        {
            return list[rng.Next(list.Count)];
        }

        public static void Add<T>(this IList<T> list, T item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(item);
            }
        }

        public static string ToString(this double v)
        {
            return v.ToString("#.##");
        }
    }
}
