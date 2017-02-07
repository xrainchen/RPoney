using System.Runtime.Remoting.Messaging;

namespace RPoney.Log.Threading
{
    /// <summary>
    /// CallContextStorage
    /// </summary>
    public class CallContextStorage : IThreadStorage
    {
        // Methods
        public void FreeNamedDataSlot(string name)
        {
            CallContext.FreeNamedDataSlot(name);
        }
        public object GetData(string name)
        {
            return CallContext.GetData(name);
        }
        public void SetData(string name, object value)
        {
            CallContext.SetData(name, value);
        }
    }





}
