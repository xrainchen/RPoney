using System;
using System.ServiceProcess;

namespace Rponey.Quartz.Service
{
    partial class ServiceManager : ServiceBase
    {
        private TaskJobHandler _battkTaskHandler;
        public ServiceManager()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //对战服务 
                _battkTaskHandler = new TaskJobHandler("");
                _battkTaskHandler.Start();
                //LoggerManager.Info(GetType().Name,$"牛牛电竞 Window服务 {PublicEnum.GlobalSettingKeyEnum.ServiceBattleScheduleEnabled.GetRemark()} 启动成功");

            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnStop()
        {
            try
            {
                _battkTaskHandler?.Stop();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
