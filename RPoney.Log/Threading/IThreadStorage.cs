namespace RPoney.Log.Threading
{
    /// <summary>
    /// 线程存储
    /// </summary>
    public interface IThreadStorage
    {
        /// <summary>
        /// 释放指定名称的数据
        /// </summary>
        /// <param name="name"></param>
        void FreeNamedDataSlot(string name);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object GetData(string name);
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SetData(string name, object value);
    }
}
