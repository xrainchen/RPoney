using System;

namespace RPoney.Utilty.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 2000000; i++)
            {
                var orderNo = Tools.CreateOrderNo();
                if (i % 50000 == 0)
                {
                    Console.WriteLine($"{i} 订单号：{orderNo}");
                }
            }
            Console.Read();
        }
    }
}
