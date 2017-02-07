using System;
using System.Web;
using RPoney.Log.Threading.Imp;

namespace RPoney.Log.Threading
{
    public sealed class LogicalThreadContext
    {
        // Fields
        private static readonly IThreadStorage httpThreadStorage = new HttpContextStorage();
        private static readonly IThreadStorage threadStorage = new CallContextStorage();

        // Methods
        private LogicalThreadContext()
        {
            throw new NotSupportedException("must not be instantiated");
        }

        public static void FreeNamedDataSlot(string name)
        {
            ThreadStorage.FreeNamedDataSlot(name);
        }

        public static object GetData(string name)
        {
            return ThreadStorage.GetData(name);
        }

        public static void SetData(string name, object value)
        {
            ThreadStorage.SetData(name, value);
        }

        // Properties
        public static IThreadStorage ThreadStorage
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return httpThreadStorage;
                }
                return threadStorage;
            }
        }
    }



}
