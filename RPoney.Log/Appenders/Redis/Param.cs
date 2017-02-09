using System;

namespace RPoney.Log.Appenders.Redis
{
    [Serializable]
    public class Param
    {
        // Properties
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
