using System.Web;
using RPoney.Log.Threading;
using RPoney.Log.Threading.Imp;

namespace RPoney.Log
{
    public sealed class ContextManager
    {
        // Fields
        private const string CONTEXT_KEY = "TOP_Context";
        private static IThreadStorage httpThreadStorage = new HttpContextStorage();
        private static IThreadStorage threadStorage = new CallContextStorage();

        // Methods
        private ContextManager()
        {
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

        public static TopContext TopContext
        {
            get
            {
                return (TopContext)GetData("TOP_Context");
            }
            set
            {
                SetData("TOP_Context", value);
            }
        }
    }
}
