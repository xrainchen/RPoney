using System;

namespace RPoney.Log.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RPoney.Log.LoggerManager.Error("Program", "sdf");
            Console.Read();
        }
    }
}
