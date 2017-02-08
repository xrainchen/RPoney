using System;
using log4net;
using log4net.Repository.Hierarchy;

namespace RPoney.Log.Imp
{
    /// <summary>
    /// log4net日志记录者
    /// </summary>
    internal class Log4netLogger : ILogger
    {
        // Fields
        private ILog _logger;

        // Methods
        public Log4netLogger(ILog log)
        {
            Level = "";
            _logger = log;
            Level = ((Logger)log.Logger).Level.ToString();
        }

        public Log4netLogger(string loggerName, string level)
        {
            Level = "";
            _logger = LogManager.GetLogger(loggerName);
            Level = level;
        }

        public void Debug(object msg)
        {
            _logger.Debug(msg);
        }

        public void Debug(object msg, Exception exception)
        {
            _logger.Debug(msg, exception);
        }

        public void Dispose()
        {
            _logger = null;
        }

        public void Error(object msg)
        {
            _logger.Error(msg);
        }

        public void Error(object msg, Exception exception)
        {
            _logger.Error(msg, exception);
        }

        public void Fatal(object msg)
        {
            _logger.Fatal(msg);
        }

        public void Fatal(object msg, Exception exception)
        {
            _logger.Fatal(msg, exception);
        }

        public void Info(object msg)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Info(msg);
            }
        }

        public void Info(object msg, Exception exception)
        {
            _logger.Info(msg, exception);
        }

        public void Warn(object msg)
        {
            _logger.Warn(msg);
        }

        public void Warn(object msg, Exception exception)
        {
            _logger.Warn(msg, exception);
        }

        // Properties
        public bool IsDebugEnabled => _logger.IsDebugEnabled;

        public bool IsErrorEnabled => _logger.IsErrorEnabled;

        public bool IsFatalEnabled => _logger.IsFatalEnabled;

        public bool IsInfoEnabled => _logger.IsInfoEnabled;

        public bool IsWarnEnabled => _logger.IsWarnEnabled;

        public string Level { get; }

        public string Name => _logger.Logger.Name;
    }



}
