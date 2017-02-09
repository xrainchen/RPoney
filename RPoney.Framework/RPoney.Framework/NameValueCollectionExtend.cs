using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace RPoney
{
    public static class NameValueCollectionExtend
    {
        public static NameValueCollection Concat(this NameValueCollection left, NameValueCollection right)
        {
            if (right != null)
            {
                foreach (var str in right.AllKeys)
                {
                    left[str] = right[str];
                }
            }
            return left;
        }

        public static NameValueCollection Copy(this NameValueCollection nvc)
        {
            NameValueCollection values = new NameValueCollection();
            if (nvc != null)
            {
                foreach (var str in nvc.AllKeys)
                {
                    values[str] = nvc[str];
                }
            }
            return values;
        }

        public static NameValueCollection RemoveRange(this NameValueCollection left, params string[] keys)
        {
            if (((left != null) && (keys != null)) && (keys.Length != 0))
            {
                foreach (var str in keys)
                {
                    if (left[str] != null)
                    {
                        left.Remove(str);
                    }
                }
            }
            return left;
        }

        public static Dictionary<string, string> ToDictionary(this NameValueCollection nvc) => nvc.Keys.Cast<string>().ToDictionary(key => key, key => nvc[key]);

        public static Dictionary<string, T> ToDictionary<T>(this NameValueCollection nvc, Func<string, T> convertor) => nvc.Keys.Cast<string>().ToDictionary(key => key, key => convertor(nvc[key]));
    }
}
