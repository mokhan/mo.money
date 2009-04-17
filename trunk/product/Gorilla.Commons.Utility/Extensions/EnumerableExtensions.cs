using System;
using System.Collections.Generic;
using System.Linq;

namespace Gorilla.Commons.Utility.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> where<T>(this IEnumerable<T> items, Func<T, bool> condition_is_met)
        {
            return null == items ? new List<T>() : items.Where(condition_is_met);
        }

        public static IList<T> databind<T>(this IEnumerable<T> items_to_bind_to)
        {
            return items_to_bind_to.ToList();
        }

        public static IEnumerable<T> that_satisfy<T>(this IEnumerable<T> items_to_peek_in_to,
                                                     Predicate<T> criteria_to_satisfy)
        {
            foreach (var item in items_to_peek_in_to ?? new List<T>())
                if (item.satisfies(criteria_to_satisfy)) yield return item;
        }

        public static IEnumerable<T> sorted_using<T>(this IEnumerable<T> items_to_sort, IComparer<T> sorting_algorithm)
        {
            var sorted_items = new List<T>(items_to_sort);
            sorted_items.Sort(sorting_algorithm);
            return sorted_items;
        }

        public static IEnumerable<T> all<T>(this IEnumerable<T> items)
        {
            foreach (var item in items ?? new List<T>()) yield return item;
        }

        public static void each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items ?? new List<T>()) action(item);
        }

        public static IEnumerable<T> join_with<T>(this IEnumerable<T> left, IEnumerable<T> right)
        {
            if (null == right) return left;

            var list = new List<T>();
            list.AddRange(left);
            list.AddRange(right);
            return list;
        }
    }
}