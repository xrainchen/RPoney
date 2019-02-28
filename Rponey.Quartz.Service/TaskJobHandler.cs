using System;
using Quartz;
using Quartz.Impl;

namespace Rponey.Quartz.Service
{
    /// <summary>
    /// 任务处理器
    /// </summary>
    public class TaskJobHandler
    {
        private const string DefaultCronExpression = "0/5 * * * * ?";
        private IScheduler _sched;

        public TaskJobHandler(string cronExpression)
        {
            CronExpression = string.IsNullOrEmpty(cronExpression) ? DefaultCronExpression : cronExpression;
        }

        public string CronExpression { set; get; }

        public void Start()
        {
            try
            {
                //设置自动执行日期
                //调度器构造工厂
                ISchedulerFactory sf = new StdSchedulerFactory();
                //第一步：构造调度器
                _sched = sf.GetScheduler();
                _sched.Start(); //启动调度器
                //第二步：定义任务
                var jobDetail = new JobDetailImpl($"Exec{GetType().Name}", typeof(TaskJobService));
                //第三步：定义触发器
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(typeof(TaskJobService).Name) //触发器名称
                    .ForJob(jobDetail)
                    .StartNow()
                    .WithCronSchedule(CronExpression) //时间表达式
                    .Build();
                _sched.ScheduleJob(jobDetail, trigger);
                //LoggerManager.Info(GetType().ToString(),$"定时自动处理  对战服务  下一次执行时间：{trigger.GetNextFireTimeUtc()?.AddHours(8)}");
            }
            catch (Exception ex)
            {

            }
        }

        public void Stop()
        {
            try
            {
                //关闭开关
                _sched?.Shutdown();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
