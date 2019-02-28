using System;
using System.IO;
using RPoney.Log;

namespace RPoney.Cache
{
    public class CacheManager
    {
        private static ICacheService _instance { get; set; }

        /// <summary>
        /// 初始化缓存配置
        /// </summary>
        /// <param name="configFilePath"></param>
        public static void Init(string configFilePath)
        {
            var description = "初始化缓存";
            try
            {
                //1.检测文件
                if (!File.Exists(configFilePath))
                {
                    LoggerManager.Error(typeof(CacheManager).Name, $"{description} 配置文件不存在 configFilePath:{configFilePath}");
                    return;
                }
                //2.读取文件内容
                var content = File.ReadAllText(configFilePath).Replace("\r\n","");
                _instance = new CacheService(/*content.DeserializeFromJSON<CacheConfigModel>()*/);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(typeof(CacheManager).Name, $"{description} 异常", ex);
            }
        }
        public static ICacheService Instance => _instance;
    }
}
