using System;
using log4net.Core;

namespace RPoney.Log.Appenders
{
    /// <summary>
    /// 日志模型
    /// </summary>
    internal class LogModel
    {
        // Fields
        internal static string ConstComputerName;
        internal static int ConstRuntime;

        // Methods
        internal static LogModel TransEventToLogObject(LoggingEvent loggingEvent)
        {
            var model = new LogModel();
            var message = loggingEvent.MessageObject as TopLogMessage;
            if (message != null)
            {
                model.BusinessType = message.BizType;
                model.MessageObject = message.TopContext;
                model.Message = message.Description;
                model.EventNo = message.EventNo;
            }
            model.LevelName = loggingEvent.Level.Name;
            var span = (DateTime.Now - new DateTime(1970, 1, 1));
            model.LogDateTime = span.Ticks / 10000000M;
            model.Exception = loggingEvent.ExceptionObject?.ToString() ?? string.Empty;
            model.ComputerName = ConstComputerName;
            model.Runtime = ConstRuntime;
            model.TypeName = "ESLog-" + DateTime.Now.ToString("yyyyMMdd");
            return model;
        }

        // Properties
        public string BusinessType { get; set; }

        public string ComputerName { get; set; }

        public string EventNo { get; set; }

        public string Exception { get; set; }

        public string IndexName { get; set; }

        public string LevelName { get; set; }

        public decimal LogDateTime { get; set; }

        public string Message { get; set; }

        public object MessageObject { get; set; }

        public int Runtime { get; set; }

        public string TypeName { get; set; }
    }
    
    /// <summary>
    /// 日志级别枚举
    /// </summary>
    public enum LogLevelEnum
    {
        Info,
        Debug,
        Warning,
        Error,
        Fatal
    }
}
