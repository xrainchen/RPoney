using System;
using System.Diagnostics;

namespace RPoney.Log
{
    public class LoggerManager
    {
        // Methods
        public static void Debug(string bizType, string description)
        {
            Debug(bizType, description, "Default");
        }

        public static void Debug(string bizType, string description, Exception exception)
        {
            Debug(bizType, description, exception, "Default");
        }

        public static void Debug(string bizType, string description, string loggerName)
        {
            Debug(bizType, description, typeof(LoggerManager), loggerName);
        }

        public static void Debug(string bizType, string description, Type type)
        {
            Debug(bizType, description, type, "Default");
        }

        public static void Debug(string bizType, string description, Exception exception, string loggerName)
        {
            Debug(bizType, description, exception, typeof(LoggerManager), loggerName);
        }

        public static void Debug(string bizType, string description, Exception exception, Type type)
        {
            Debug(bizType, description, exception, type, "Default");
        }

        public static void Debug(string bizType, string description, Type type, string loggerName)
        {
            try
            {
                ILogger logger = LoggerFactory.Instance.GetLogger(loggerName);
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (logger.IsDebugEnabled)
                {
                    msg.TopContext = ContextManager.TopContext;
                    if (ContextManager.TopContext != null)
                    {
                        msg.EventNo = ContextManager.TopContext.EventNo;
                    }
                    logger.Debug(msg);
                }
            }
            catch (Exception exception)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception.Message);
            }
        }

        public static void Debug(string bizType, string description, Exception exception, Type type, string loggerName)
        {
            try
            {
                ILogger logger = LoggerFactory.Instance.GetLogger(loggerName);
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (logger.IsDebugEnabled)
                {
                    msg.TopContext = ContextManager.TopContext;
                    if (ContextManager.TopContext != null)
                    {
                        msg.EventNo = ContextManager.TopContext.EventNo;
                    }
                    logger.Debug(msg, exception);
                }
            }
            catch (Exception exception2)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception2.Message);
            }
        }

        public static void Error(string bizType, string description)
        {
            Error(bizType, description, "Default");
        }

        public static void Error(string bizType, string description, Exception exception)
        {
            Error(bizType, description, exception, "Default");
        }

        public static void Error(string bizType, string description, string loggerName)
        {
            Error(bizType, description, typeof(LoggerManager), loggerName);
        }

        public static void Error(string bizType, string description, Type type)
        {
            Error(bizType, description, type, "Default");
        }

        public static void Error(string bizType, string description, Exception exception, string loggerName)
        {
            Error(bizType, description, exception, typeof(LoggerManager), loggerName);
        }

        public static void Error(string bizType, string description, Exception exception, Type type)
        {
            Error(bizType, description, exception, type, "Default");
        }

        public static void Error(string bizType, string description, Type type, string loggerName)
        {
            try
            {
                ILogger logger = LoggerFactory.Instance.GetLogger(loggerName);
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (logger.IsErrorEnabled)
                {
                    msg.TopContext = ContextManager.TopContext;
                    if (ContextManager.TopContext != null)
                    {
                        msg.EventNo = ContextManager.TopContext.EventNo;
                    }
                    logger.Error(msg);
                }
            }
            catch (Exception exception)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception.Message);
            }
        }

        public static void Error(string bizType, string description, Exception exception, Type type, string loggerName)
        {
            try
            {
                ILogger logger = LoggerFactory.Instance.GetLogger(loggerName);
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (logger.IsErrorEnabled)
                {
                    msg.TopContext = ContextManager.TopContext;
                    if (ContextManager.TopContext != null)
                    {
                        msg.EventNo = ContextManager.TopContext.EventNo;
                    }
                    logger.Error(msg, exception);
                }
            }
            catch (Exception exception2)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception2.Message);
            }
        }

        public static void Fatal(string bizType, string description)
        {
            Fatal(bizType, description, "Default");
        }

        public static void Fatal(string bizType, string description, Exception exception)
        {
            Fatal(bizType, description, exception, "Default");
        }

        public static void Fatal(string bizType, string description, string loggerName)
        {
            Fatal(bizType, description, typeof(LoggerManager), loggerName);
        }

        public static void Fatal(string bizType, string description, Type type)
        {
            Fatal(bizType, description, type, "Default");
        }

        public static void Fatal(string bizType, string description, Exception exception, string loggerName)
        {
            Fatal(bizType, description, exception, typeof(LoggerManager), loggerName);
        }

        public static void Fatal(string bizType, string description, Exception exception, Type type)
        {
            Fatal(bizType, description, exception, type, "Default");
        }

        public static void Fatal(string bizType, string description, Type type, string loggerName)
        {
            try
            {
                ILogger logger = LoggerFactory.Instance.GetLogger(loggerName);
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (logger.IsFatalEnabled)
                {
                    msg.TopContext = ContextManager.TopContext;
                    if (ContextManager.TopContext != null)
                    {
                        msg.EventNo = ContextManager.TopContext.EventNo;
                    }
                    logger.Fatal(msg);
                }
            }
            catch (Exception exception)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception.Message);
            }
        }

        public static void Fatal(string bizType, string description, Exception exception, Type type, string loggerName)
        {
            try
            {
                ILogger logger = LoggerFactory.Instance.GetLogger(loggerName);
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (logger.IsFatalEnabled)
                {
                    msg.TopContext = ContextManager.TopContext;
                    if (ContextManager.TopContext != null)
                    {
                        msg.EventNo = ContextManager.TopContext.EventNo;
                    }
                    logger.Fatal(msg, exception);
                }
            }
            catch (Exception exception2)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception2.Message);
            }
        }

        public static void Info(string bizType, string description)
        {
            Info(bizType, description, "Default");
        }

        public static void Info(string bizType, string description, Exception exception)
        {
            Info(bizType, description, exception, "Default");
        }

        public static void Info(string bizType, string description, string loggerName)
        {
            Info(bizType, description, typeof(LoggerManager), loggerName);
        }

        public static void Info(string bizType, string description, Type type)
        {
            Info(bizType, description, type, "Default");
        }

        public static void Info(string bizType, string description, Exception exception, string loggerName)
        {
            Info(bizType, description, exception, typeof(LoggerManager), loggerName);
        }

        public static void Info(string bizType, string description, Exception exception, Type type)
        {
            Info(bizType, description, exception, type, "Default");
        }

        public static void Info(string bizType, string description, Type type, string loggerName)
        {
            try
            {
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (ContextManager.TopContext != null)
                {
                    msg.EventNo = ContextManager.TopContext.EventNo;
                }
                LoggerFactory.Instance.GetLogger(loggerName).Info(msg);
            }
            catch (Exception exception)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception.Message);
            }
        }

        public static void Info(string bizType, string description, Exception exception, Type type, string loggerName)
        {
            try
            {
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (ContextManager.TopContext != null)
                {
                    msg.EventNo = ContextManager.TopContext.EventNo;
                }
                LoggerFactory.Instance.GetLogger(loggerName).Info(msg, exception);
            }
            catch (Exception exception2)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception2.Message);
            }
        }

        public static void Warn(string bizType, string description)
        {
            Warn(bizType, description, "Default");
        }

        public static void Warn(string bizType, string description, Exception exception)
        {
            Warn(bizType, description, exception, "Default");
        }

        public static void Warn(string bizType, string description, string loggerName)
        {
            Warn(bizType, description, typeof(LoggerManager), loggerName);
        }

        public static void Warn(string bizType, string description, Type type)
        {
            Warn(bizType, description, type, "Default");
        }

        public static void Warn(string bizType, string description, Exception exception, string loggerName)
        {
            Warn(bizType, description, exception, typeof(LoggerManager), loggerName);
        }

        public static void Warn(string bizType, string description, Exception exception, Type type)
        {
            Warn(bizType, description, exception, type, "Default");
        }

        public static void Warn(string bizType, string description, Type type, string loggerName)
        {
            try
            {
                ILogger logger = LoggerFactory.Instance.GetLogger(loggerName);
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (logger.IsWarnEnabled)
                {
                    msg.TopContext = ContextManager.TopContext;
                    if (ContextManager.TopContext != null)
                    {
                        msg.EventNo = ContextManager.TopContext.EventNo;
                    }
                    logger.Warn(msg);
                }
            }
            catch (Exception exception)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception.Message);
            }
        }

        public static void Warn(string bizType, string description, Exception exception, Type type, string loggerName)
        {
            try
            {
                ILogger logger = LoggerFactory.Instance.GetLogger(loggerName);
                TopLogMessage msg = new TopLogMessage(bizType, description);
                if (type != null)
                {
                    msg.ClassName = type.FullName;
                }
                if (logger.IsWarnEnabled)
                {
                    msg.TopContext = ContextManager.TopContext;
                    if (ContextManager.TopContext != null)
                    {
                        msg.EventNo = ContextManager.TopContext.EventNo;
                    }
                    logger.Warn(msg, exception);
                }
            }
            catch (Exception exception2)
            {
                Debugger.Log(0, typeof(LoggerManager).Name, typeof(LoggerManager).Name + ":" + exception2.Message);
            }
        }
    }

}
