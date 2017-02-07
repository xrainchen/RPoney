using System.Web;

namespace RPoney.Log.Threading.Imp
{
    /// <summary>
    /// HttpContextStorage
    /// </summary>
    public class HttpContextStorage : IThreadStorage
    {
        // Methods
        public void FreeNamedDataSlot(string name)
        {
            HttpContext.Current.Items.Remove(name);
        }

        public object GetData(string name)
        {
            return HttpContext.Current.Items[name];
        }

        public void SetData(string name, object value)
        {
            HttpContext.Current.Items[name] = value;
        }
    }

}
