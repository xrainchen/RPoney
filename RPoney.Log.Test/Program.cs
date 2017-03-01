using System;

namespace RPoney.Log.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RPoney.Log.LoggerManager.Debug("Program", "Debug");
            RPoney.Log.LoggerManager.Info("Program", "Info");
            RPoney.Log.LoggerManager.Warn("Program", "Warn");
            RPoney.Log.LoggerManager.Error("Program", "Error");
            RPoney.Log.LoggerManager.Fatal("Program", "Fatal");
            Console.Read();
        }
    }
}
