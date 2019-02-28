using System;
using System.ServiceProcess;

namespace Rponey.Quartz.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var handler = new TaskJobHandler("");
                handler.Start();
                Console.Read();
                handler.Stop();
            }
            else
            {
                var servicesToRun = new ServiceBase[] { new ServiceManager() };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
