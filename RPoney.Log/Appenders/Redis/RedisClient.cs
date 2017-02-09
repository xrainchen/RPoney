using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using ServiceStack.Common.Extensions;
using ServiceStack.Redis;

namespace RPoney.Log.Appenders.Redis
{
    internal class RedisClient : ILogClient
    {
        // Fields
        private PooledRedisClientManager clientManager;
        private Config Config;
        private readonly string configFile = AppDomain.CurrentDomain.BaseDirectory + @"\Config\FzCyjhRedisLog.Config";// @"D:\Configs\Log\FzCyjhRedisLog.config";
        private const string ListId = "logstash";
        // Methods
        public RedisClient()
        {
            Init();
        }

        public void AddValue(LogModel value)
        {
            if (value != null)
            {
                using (var client = clientManager.GetClient())
                {
                    client.EnqueueItemOnList(ListId, value.SerializeToJSON());
                }
            }
        }

        public void AddValues(LogModel[] values)
        {
            values.ForEach(AddValue);
        }

        public LogModel GetLogModel()
        {
            using (var client = clientManager.GetClient())
            {
                string str = client.DequeueItemFromList(ListId);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                return str.DeserializeFromJSON<LogModel>();
            }
        }

        public void Init()
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(configFile, FileMode.Open, FileAccess.Read);
                Config config = new XmlSerializer(typeof(Config)).Deserialize(stream) as Config;
                if (config == null)
                {
                    throw new Exception("RPoneyRedisLog.config配置文件");
                }
                LogModel.ConstRuntime = config.Runtime;
                LogModel.ConstComputerName = Dns.GetHostName();
                Init(config);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        private void Init(Config config)
        {
            Config = config;
            var separator = new char[] { ',' };
            var readWriteHosts = config.GetParamValue("WriteServer").Split(separator);
            var chArray2 = new char[] { ',' };
            var readOnlyHosts = config.GetParamValue("ReadServer").Split(chArray2);
            var paramValue = config.GetParamValue("AutoStart");
            var str2 = config.GetParamValue("DefaultDb");
            var str3 = config.GetParamValue("MaxReadPoolSize");
            var str4 = config.GetParamValue("MaxWritePoolSize");
            var config2 = new RedisClientManagerConfig();
            if (!string.IsNullOrEmpty(paramValue))
            {
                config2.AutoStart = bool.Parse(paramValue);
            }
            if (!string.IsNullOrEmpty(str2))
            {
                config2.DefaultDb = long.Parse(str2);
            }
            if (!string.IsNullOrEmpty(str3))
            {
                config2.MaxReadPoolSize = int.Parse(str3);
            }
            if (!string.IsNullOrEmpty(str4))
            {
                config2.MaxWritePoolSize = int.Parse(str4);
            }
            clientManager = new PooledRedisClientManager(readWriteHosts, readOnlyHosts, config2);
        }
        public string ESUrl => Config.ESUrl;
    }


}
