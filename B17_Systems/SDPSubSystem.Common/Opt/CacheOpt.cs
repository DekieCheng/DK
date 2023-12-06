using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace SDPSubSystem.Common.Opt
{
    /// <summary>
    /// 缓存技术类
    /// </summary>
    public class CacheOpt
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objValue"></param>
        public static void SetCache(string CacheKey, object objValue)
        {
            HttpRuntime.Cache.Insert(CacheKey, objValue);
        }
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objValue"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        public static void SetCache(string CacheKey, object objValue, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objValue, null, absoluteExpiration, slidingExpiration);
        }

        public static void SetCache(string CacheKey, object objValue, string fileName)
        {
            System.Web.Caching.CacheDependency cadep = new CacheDependency(fileName);
            HttpRuntime.Cache.Insert(CacheKey, objValue, cadep);
        }
        /// <summary>
        /// 移除指定缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        public static void RemoveCache(string CacheKey)
        {
            HttpRuntime.Cache.Remove(CacheKey);
        }
        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}
