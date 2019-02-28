using System;
using System.Linq;

namespace RPoney.Utilty.Extend
{
    public static class IntegerExtend
    {
        public static T ToEnum<T>(this int enumValue)
        {
            var t = typeof(T);

            foreach (var e in from object e in Enum.GetValues(t) where (int)e == enumValue select e)
            {
                return (T)Enum.Parse(t, ((int)e).ToString());
            }

            return default(T);
        }
    }
}
