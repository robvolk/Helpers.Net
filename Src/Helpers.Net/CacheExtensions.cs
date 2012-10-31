using System.Web.Caching;

namespace System.Web
{
    public static class CacheExtensions
    {
        static object sync = new object();

        /// <summary>
        /// Executes a method and stores the result in cache using the given cache key.  If the data already exists in cache, it returns the data
        /// and doesn't execute the method.  Thread safe, although the method parameter isn't guaranteed to be thread safe.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">Cache from HttpContext.  If null, method is executed directly.</param>
        /// <param name="cacheKey">Each method has it's own isolated set of cache items, so cacheKeys won't overlap accross methods.</param>
        /// <param name="method"></param>
        /// <param name="expirationSeconds">Lifetime of cache items, in seconds</param>
        /// <returns></returns>
        public static T Data<T>(this Cache cache, string cacheKey, int expirationSeconds, Func<T> method)
        {
            var data = cache == null ? default(T) : (T)cache[cacheKey];
            if (data == null)
            {
                data = method();

                if (expirationSeconds > 0 && data != null)
                {
                    lock (sync)
                    {
                        cache.Insert(cacheKey, data, null, DateTime.Now.AddSeconds(expirationSeconds), Cache.NoSlidingExpiration);
                    }
                }
            }
            return data;
        }
    }
}
