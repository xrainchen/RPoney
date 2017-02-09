using log4net.Appender;
using log4net.Core;

namespace RPoney.Log.Appenders.Redis
{
    /// <summary>
    /// Redis输出
    /// </summary>
    public class RedisAppender : AppenderSkeleton
    {
        private static readonly ILogClient Client = new RedisClient();
        protected override void Append(LoggingEvent loggingEvent)
        {
            var model = LogModel.TransEventToLogObject(loggingEvent);
            model.IndexName = "rponey-" + this.ProjectName.ToLowerInvariant();
            Client.AddValue(model);
        }
        public string ProjectName { get; set; }
    }
}
