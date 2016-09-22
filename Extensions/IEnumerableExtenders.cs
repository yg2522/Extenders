using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utility.Extenders
{
    public static class IEnumerableExtenders
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<T> IntersectBy<T, O, P>(this IEnumerable<T> source, IEnumerable<O> other, Func<T, P> tProp, Func<O, P> oProp)
        {
            var set = new HashSet<P>(other.Select<O, P>(o => oProp(o)));

            foreach (var t in source)
                if (set.Remove(tProp(t)))
                    yield return t;
        }

        public static IEnumerable<T> IntersectBy<T, O, P>(this IEnumerable<T> source, IEnumerable<O> other, Func<T, O, bool> compare)
        {
            var set = new HashSet<O>(other);

            foreach (var t in source)
            {
                if (set.Remove(set.FirstOrDefault(x => compare == null ? x.Equals(t) : compare(t, x))))
                    yield return t;
            }
        }

        public static IEnumerable<T> ExceptBy<T, O, P>(this IEnumerable<T> source, IEnumerable<O> other, Func<T, P> tProp, Func<O, P> oProp)
        {
            var set = new HashSet<P>(other.Select<O, P>(o => oProp(o)));

            foreach (var t in source)
                if (!set.Remove(tProp(t)))
                    yield return t;
        }

        public static IEnumerable<T> ExceptBy<T, O, P>(this IEnumerable<T> source, IEnumerable<O> other, Func<T, O, bool> compare)
        {
            var set = new HashSet<O>(other);

            foreach (var t in source)
            {
                if (!set.Remove(set.FirstOrDefault(x => compare == null ? x.Equals(t) : compare(t, x))))
                    yield return t;
            }
        }

        public static bool EqualsBy<T, O, P>(this IEnumerable<T> source, IEnumerable<O> other, Func<T, P> tProp, Func<O, P> oProp)
        {
            if (source.Count() != other.Count())
                return false;

            var set = new HashSet<P>(other.Select<O, P>(o => oProp(o)));

            foreach (var t in source)
                if (!set.Remove(tProp(t)))
                    return false;

            return true;
        }

        public static bool EqualsBy<T, O>(this IEnumerable<T> source, IEnumerable<O> other, Func<T, O, bool> compare)
        {
            if (source.Count() != other.Count())
                return false;

            var compareset = new HashSet<O>(other);

            foreach (var t in source)
            {
                if (!compareset.Remove(compareset.FirstOrDefault(x => compare == null ? x.Equals(t) : compare(t, x))))
                    return false;
            }
            return true;
        }

        public static IEnumerable<T> CastIf<T>(this IEnumerable source)
        {
            var typeoft = typeof(T);
            foreach (var t in source)
            {
                if (typeoft.IsAssignableFrom(t.GetType()))
                {
                    yield return (T)t;
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            if (enumeration == null) return;
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static IEnumerable<T> ConsecutiveBy<T>(this IEnumerable<T> sequence, Func<T, int> selector, int count = 0)
        {
            if (sequence == null || sequence.Count() == 0)
                yield break;

            int maxcount = sequence.Count();
            if (count <= 0)
                count = maxcount;

            T previous = sequence.First();
            int counter = 1;
            yield return previous;

            foreach (T current in sequence.Skip(1))
            {
                if (selector(current) == selector(previous) + 1 && counter < count)
                {
                    previous = current;
                    counter++;
                    yield return current;
                }
                else
                {
                    yield break;
                }
            }
            
        }
    }
}
