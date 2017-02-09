using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RPoney.Log.Appenders.Redis
{
    [Serializable]
    public class Config
    {
        public Config()
        {
            Params = new List<Param>();
        }

        public string GetParamValue(string key) => (from p in this.Params where p.Key == key select p.Value).FirstOrDefault();

        public string EsUrl { get; set; }

        public List<Param> Params { get; set; }

        public int Runtime { get; set; }

        // Nested Types
        [Serializable, CompilerGenerated]
        private sealed class NestedConfig
        {
            public static readonly NestedConfig Info=new NestedConfig();
            public static Func<Param, string> DicInfo;
            public string GetParamValue(Param param)
            {
                return param.Value;
            }
        }
    }
}
