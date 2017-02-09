using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace RPoney.Log.Imp
{
    /// <summary>
    /// log4net工厂
    /// </summary>
    internal class Log4netFactory : ILoggerFactory
    {
        // Fields
        private static Dictionary<string, IAppender> _AppenderList = new Dictionary<string, IAppender>();
        private static Dictionary<string, log4net.Core.ILogger> _Log4netLogList = new Dictionary<string, log4net.Core.ILogger>();
        private static Dictionary<string, ILogger> _LogList = new Dictionary<string, ILogger>();

        // Methods
        public Log4netFactory() : this(AppDomain.CurrentDomain.BaseDirectory + @"\Config\log4net.Config")
        {
        }

        public Log4netFactory(string configPath)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(configPath));
            InitLog4net();
        }

        public void ChangeAppender(string loggerName, string appenderName)
        {
            if (!_Log4netLogList.ContainsKey(loggerName))
            {
                throw new KeyNotFoundException("找不到[" + loggerName + "]关键字！");
            }
            Logger logger1 = _Log4netLogList[loggerName] as Logger;
            logger1.RemoveAllAppenders();
            logger1.AddAppender(_AppenderList[appenderName]);
        }

        public void ChangeConfig(string xml)
        {
            XmlConfigurator.Configure(new MemoryStream(Encoding.Default.GetBytes(xml)));
            Clear();
            InitLog4net();
        }

        public void ChangeLevel(string loggerName, string levelName)
        {
            if (!_Log4netLogList.ContainsKey(loggerName))
            {
                throw new KeyNotFoundException("找不到[" + loggerName + "]关键字！");
            }
            ((Logger)_Log4netLogList[loggerName]).Level = ((Logger)_Log4netLogList[loggerName]).Repository.LevelMap[levelName];
        }

        private void Clear()
        {
            _AppenderList.Clear();
            _Log4netLogList.Clear();
            _LogList.Clear();
        }

        public ILogger GetLogger(string loggerName)
        {
            if (!_LogList.ContainsKey(loggerName))
            {
                throw new KeyNotFoundException("找不到[" + loggerName + "]关键字！");
            }
            return _LogList[loggerName];
        }

        private void InitLog4net()
        {
            ILoggerRepository[] allRepositories = LogManager.GetAllRepositories();
            for (int i = 0; i < allRepositories.Length; i++)
            {
                var hierarchy = (Hierarchy)allRepositories[i];
                foreach (var logger in hierarchy.GetCurrentLoggers())
                {
                    string level = (((Logger)logger).Level == null) ? string.Empty : ((Logger)logger).Level.ToString();
                    _LogList.Add(logger.Name, new Log4netLogger(logger.Name, level));
                    _Log4netLogList.Add(logger.Name, logger);
                }
                foreach (var appender in hierarchy.GetAppenders())
                {
                    if ((appender != null) && (appender.Name != null))
                    {
                        _AppenderList.Add(appender.Name, appender);
                    }
                }
            }
        }
        public Dictionary<string, ILogger> LoggerList => _LogList;
    }

}
