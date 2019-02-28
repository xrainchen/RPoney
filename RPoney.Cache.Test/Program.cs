using System;
using System.IO;
using RPoney.Cache.Model;

namespace RPoney.Cache.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CacheManager.Init(@"D:\Configs\RPoneyCache.config");
            var key = "abc";
            var testModel = new CacheTestModel
            {
                Name = "today",
                Age = 30,
                Sex = "男",
                CreatedOn = DateTime.Now

            };
            CacheManager.Instance.Add(key, testModel);
            var cacheObj=CacheManager.Instance.Get(key);
            var cacheModel = cacheObj as CacheTestModel;
            Console.Read();
        }
    }

    public class CacheTestModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
