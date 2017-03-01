using System;
using System.Collections.Generic;

namespace RPoney.Data
{
    [Serializable]
    public class CommandResult
    {
        /// <summary>
        /// 返回影响记录数
        /// </summary>
        public int NonQuery { get; set; }
        /// <summary>
        /// 输出值
        /// </summary>
        public Dictionary<string, object> OutPutValue { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public int ReturnValue { get; set; }
        /// <summary>
        /// 返回值对象
        /// </summary>
        public object ReturnValueObj { get; set; }
    }

    public enum ECloseTransactionType
    {
        Auto,
        RollBack,
        Commit,
        NoTransaction
    }
}
