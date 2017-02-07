using System;
using System.Collections;

namespace RPoney.Log.Threading.Imp
{
    /// <summary>
    /// ThreadStaticStorage
    /// </summary>
    public class ThreadStaticStorage : IThreadStorage
    {
        /// <summary>
        /// 每一个线程一个实例
        /// </summary>
        [ThreadStatic]
        private static Hashtable data;

        // Methods
        public void FreeNamedDataSlot(string name)
        {
            Data.Remove(name);
        }

        public object GetData(string name)
        {
            return Data[name];
        }

        public void SetData(string name, object value)
        {
            Data[name] = value;
        }

        // Properties
        private static Hashtable Data
        {
            get
            {
                if (data == null)
                {
                    data = new Hashtable();
                }
                return data;
            }
        }
    }



}
