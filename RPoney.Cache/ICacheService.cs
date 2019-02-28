using System;
using System.IO;
using System.Web;
using RPoney.Cache.Model;
using RPoney.Log;

namespace RPoney.Cache
{
    public interface ICacheService
    {
        /// <summary>
        /// 添加Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="expireOn">过期时间</param>
        void Add(string key, object data, TimeSpan? expireOn = null);
        /// <summary>
        /// 移除Key
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);
    }

    public class CacheService : ICacheService
    {
        public void Add(string key, object data, TimeSpan? expireOn = null)
        {
            try
            {
                var cache = HttpRuntime.Cache;
                var absoluteExpiration = expireOn == null
                    ? System.Web.Caching.Cache.NoAbsoluteExpiration
                    : (DateTime.Now + expireOn.Value);//从不过期
                var slidingExpiration = expireOn ?? System.Web.Caching.Cache.NoSlidingExpiration; //禁用可调过期
                cache.Insert(
                     key,
                     data,
                     null,
                    absoluteExpiration,
                    slidingExpiration,
                     System.Web.Caching.CacheItemPriority.Default,
                     null);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"添加缓存异常 key:{key}", ex);
            }
        }

        public void Remove(string key)
        {
            try
            {
                var cache = HttpRuntime.Cache;
                cache.Remove(key);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"移除缓存异常 key:{key}", ex);
            }
        }

        public object Get(string key)
        {
            try
            {
                var cache = HttpRuntime.Cache;
                return cache.Get(key);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"移除缓存异常 key:{key}", ex);
                return null;
            }
        }
        ///// <summary>
        ///// 缓存键
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //private string GetCacheKey(string key)
        //{
        //    return _config.VirtualPath + key.Md5Lower() + ".cache";
        //}
        ///// <summary>
        ///// 依赖键
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //private string GetDependencyKey(string key)
        //{
        //    return (_config.VirtualPath + key.Md5Lower()).Md5Lower()+".dependencyCache";
        //}
    }
}
