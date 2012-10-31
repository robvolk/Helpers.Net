using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first,
            IEnumerable<TSource> second, Func<TSource, TSource, bool> comparer)
        {
            return first.Intersect(second, new LambdaComparer<TSource>(comparer));
        }

        /// <summary>
        /// Executes a method if the object is not it's default value (null for reference types).
        /// It's the same as doing: o == null ? null : o.SomeProperty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static TResult IfNotNull<T, TResult>(this T o, Func<T, TResult> method)
        {
            if (o == null || o.Equals(default(T)))
                return default(TResult);
            else
                return method(o);
        }

        /// <summary>
        /// Enumerates through a list and executes some code, similar to a foreach statement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
            return source;
        }

        /// <summary>
        /// Used to modify properties of an object returned from a LINQ query
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="input"></param>
        /// <param name="updater"></param>
        /// <returns></returns>
        public static TSource Set<TSource>(this TSource input, Action<TSource> updater)
        {
            updater(input);
            return input;
        }

        /// <summary>
        /// Determines whether an item exists in an enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool Exists<T>(this IEnumerable<T> list, T element)
        {
            return list.Count(e => e.Equals(element)) > 0;
        }

        /// <summary>
        /// Determines whether an item exists in an enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool Exists<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            return list.Count(predicate) > 0;
        }
    }
}