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
        /// <typeparam name="TResult"></typeparam>
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
        /// Used to chain an object with customization,
        /// such as modifying properties of an object returned from a LINQ query.
        /// Chaining of methods is supported since the method returns the object operated on.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="input"></param>
        /// <param name="updater"></param>
        /// <returns></returns>
        public static TSource Select<TSource>(this TSource input, Action<TSource> updater)
        {
            updater(input);
            return input;
        }

        /// <summary>
        /// Used to chain an object with customization,
        /// such as modifying properties of an object returned from a LINQ query
        /// Supports returning a different object, thus changing what object the next method
        /// in the chain operates on.
        /// Can also be used to project an object onto a lambda,
        /// removing the need for a local object reference
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="input"></param>
        /// <param name="updater"></param>
        /// <returns></returns>
        public static TResult Select<TSource, TResult>(this TSource input, Func<TSource, TResult> updater)
        {
            return updater(input);
        }

        // From https://github.com/johtela/Flop/blob/master/Flop/Base/Extensions.cs
        /// <summary>
        /// Amplifies an object to a single item enumerable, allowing LINQ methods to be
        /// used on a single object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<T> AsEnumerable<T>(this T value)
        {
            yield return value;
        }


        // From https://github.com/johtela/Flop/blob/master/Flop/Base/Extensions.cs
        /// <summary>
        /// This to avoid using Item1, Item2 of Tuple and use real variable names instead.  Example:
        /// 
        ///                 var t = Tuple.Create(42, "John");
        /// BEFORE:
        ///                 Console.WriteLine( "Name: {0}, Age: {1}", t.Item1, t.Item2);
        /// 
        /// AFTER:
        ///                 t.Bind((age, name) => Console.WriteLine( "Name: {0}, Age: {1}", age, name));
        /// 
        /// </summary>
        public static void Bind<T1, T2>(this Tuple<T1, T2> tuple, Action<T1, T2> action) { action(tuple.Item1, tuple.Item2); }
        public static void Bind<T1, T2, T3>(this Tuple<T1, T2, T3> tuple, Action<T1, T2, T3> action) { action(tuple.Item1, tuple.Item2, tuple.Item3); }
        public static void Bind<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> tuple, Action<T1, T2, T3, T4> action) { action(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4); }

        public static R Bind<T1, T2, R>(this Tuple<T1, T2> tuple, Func<T1, T2, R> func) { return func(tuple.Item1, tuple.Item2); }
        public static R Bind<T1, T2, T3, R>(this Tuple<T1, T2, T3> tuple, Func<T1, T2, T3, R> func) { return func(tuple.Item1, tuple.Item2, tuple.Item3); }
        public static R Bind<T1, T2, T3, T4, R>(this Tuple<T1, T2, T3, T4> tuple, Func<T1, T2, T3, T4, R> func) { return func(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4); }        
        
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
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool Exists<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            return list.Count(predicate) > 0;
        }
    }
}