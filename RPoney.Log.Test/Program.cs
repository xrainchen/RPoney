using System;

namespace RPoney.Log.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new
            {
                Id=1,
                Name="test"
            };
            RPoney.Log.LoggerManager.Debug("Program", $"Debug1{model.SerializeToJSON()}");
            RPoney.Log.LoggerManager.Info("Program", "Info1");
            RPoney.Log.LoggerManager.Warn("Program", "Warn1");
            try
            {
                var a = int.Parse("134f");
            }
            catch (Exception ex)
            {
                RPoney.Log.LoggerManager.Error("Program", "Error1",ex);
            }
            RPoney.Log.LoggerManager.Fatal("Program", "Fatal1");
            Console.WriteLine("记录完成");
            Console.Read();
        }
    }
}
