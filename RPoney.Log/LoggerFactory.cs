using RPoney.Log.Imp;

namespace RPoney.Log
{
    /// <summary>
    /// 记录者工厂
    /// </summary>
    internal class LoggerFactory
    {
        // Fields
        private static LoggerFactory _Instance = new LoggerFactory();
        private static ILoggerFactory _LoggerFactory = new Log4netFactory();

        // Methods
        public void ChangeAppender(string loggerName, string appenderName)
        {
            _LoggerFactory.ChangeAppender(loggerName, appenderName);
        }

        public void ChangeConfig(string xml)
        {
            _LoggerFactory.ChangeConfig(xml);
        }

        public void ChangeLevel(string loggerName, string levelName)
        {
            _LoggerFactory.ChangeLevel(loggerName, levelName);
        }

        public ILogger GetLogger(string loggerName)
        {
            return _LoggerFactory.GetLogger(loggerName);
        }

        // Properties
        public static LoggerFactory Instance => _Instance;
    }


}
