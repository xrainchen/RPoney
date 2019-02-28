using System;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace RPoney.Utilty
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 根据完整文件路径获取FileStream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileStream GetFileStream(string fileName)
        {
            FileStream fileStream = null;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            return fileStream;
        }
        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileContent(string fileName)
        {
            var cache = HttpRuntime.Cache;
            var key = fileName.Md5Lower();
            var cacheObj = cache.Get(key);
            if (cacheObj != null)
            {
                return cacheObj.ToString();
            }
            var content = File.ReadAllText(fileName);
            var mydep = new CacheDependency(fileName);
            cache.Insert(
                 key,
                 content,
                 mydep,
                 Cache.NoAbsoluteExpiration,//从不过期
                Cache.NoSlidingExpiration,//禁用可调过期
                 CacheItemPriority.Default,
                 null);
            return content;
        }
    }
}
