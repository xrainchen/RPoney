using System.Collections.Generic;

namespace RPoney.Log
{
    /// <summary>
    /// 日志记录器工厂
    /// </summary>
    public interface ILoggerFactory
    {
        // Methods
        void ChangeAppender(string loggerName, string appenderName);
        void ChangeConfig(string xml);
        void ChangeLevel(string loggerName, string levelName);
        ILogger GetLogger(string loggerName);

        // Properties
        Dictionary<string, ILogger> LoggerList { get; }
    }





}
