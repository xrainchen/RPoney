using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;

namespace RPoney
{
    public static class ObjectHelper
    {
        public static bool CBoolean(this object input, bool defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
            {
                return defaultValue;
            }
            string str = input.ToString().Trim();
            if (string.IsNullOrEmpty(str))
            {
                return defaultValue;
            }
            if (str == "1")
            {
                return true;
            }
            if (str.Equals("true", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (str.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (str.Equals("on", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (str.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (str == "0")
            {
                return false;
            }
            if (str.Equals("off", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (str.Equals("no", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (str.Equals("false", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (str.Equals("!", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            try
            {
                return Convert.ToBoolean(input);
            }
            catch (Exception exception)
            {
                if (throwEx)
                {
                    throw exception;
                }
                return defaultValue;
            }
        }

        public static bool? CBooleanOrNull(this object input)
        {
            if (input.IsDbNullOrNull())
            {
                return null;
            }
            string str = input.ToString().Trim();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            if (str == "1")
            {
                return true;
            }
            if (str.Equals("true", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (str.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (str.Equals("on", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (str.Equals("ok", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (str == "0")
            {
                return false;
            }
            if (str.Equals("off", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (str.Equals("no", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (str.Equals("false", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            if (str.Equals("!", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            try
            {
                return new bool?(Convert.ToBoolean(input));
            }
            catch
            {
                return null;
            }
        }

        public static DateTime CDateTime(this object input, DateTime defaultValue) => input.CDateTime(defaultValue, false);

        public static DateTime CDateTime(this object input, DateTime defaultValue, bool throwEx)
        {
            try
            {
                return Convert.ToDateTime(input);
            }
            catch
            {
                if (throwEx)
                {
                    throw;
                }
                return defaultValue;
            }
        }

        public static DateTime? CDateTimeOrNull(this object input)
        {
            if (input.IsDbNullOrNull())
            {
                return null;
            }
            if (string.IsNullOrEmpty(input.ToString().Trim()))
            {
                return null;
            }
            try
            {
                return new DateTime?(Convert.ToDateTime(input));
            }
            catch
            {
                return null;
            }
        }

        public static decimal CDec(this object input, decimal defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
            {
                return defaultValue;
            }
            if (string.IsNullOrEmpty(input.ToString().Trim()))
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToDecimal(input);
            }
            catch
            {
                if (throwEx)
                {
                    throw;
                }
                return defaultValue;
            }
        }

        public static decimal? CDecOrNull(this object input)
        {
            if (!input.IsDbNullOrNull())
            {
                decimal num;
                string str = input.ToString().Trim();
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                if (decimal.TryParse(str, out num))
                {
                    return new decimal?(num);
                }
            }
            return null;
        }

        public static double CDouble(this object input, double defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
            {
                return defaultValue;
            }
            if (string.IsNullOrEmpty(input.ToString().Trim()))
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToDouble(input);
            }
            catch
            {
                if (throwEx)
                {
                    throw;
                }
                return defaultValue;
            }
        }

        public static Guid? CGuidOrNull(this object input)
        {
            if (input.IsDbNullOrNull())
            {
                return null;
            }
            try
            {
                return new Guid(input.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static int CInt(this object input, int defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
            {
                return defaultValue;
            }
            if (string.IsNullOrEmpty(input.ToString().Trim()))
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                if (throwEx)
                {
                    throw;
                }
                return defaultValue;
            }
        }

        public static int? CIntOrNull(this object input)
        {
            if (!input.IsDbNullOrNull())
            {
                int num;
                string str = input.ToString().Trim();
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                if (int.TryParse(str, out num))
                {
                    return new int?(num);
                }
            }
            return null;
        }

        public static long CLong(this object input, long defaultValue, bool throwEx)
        {
            if (input.IsDbNullOrNull())
            {
                return defaultValue;
            }
            if (string.IsNullOrEmpty(input.ToString().Trim()))
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToInt64(input);
            }
            catch
            {
                if (throwEx)
                {
                    throw;
                }
                return defaultValue;
            }
        }

        public static string CString(this object input, string defaultValue)
        {
            if (!input.IsDbNullOrNull())
            {
                return input.ToString();
            }
            return defaultValue;
        }

        public static object GetValueOrDefault(this object input, object defaultValue)
        {
            if ((input != null) && (input != DBNull.Value))
            {
                return input;
            }
            return defaultValue;
        }

        public static bool IsDbNullOrNull(this object input)
        {
            if (input != null)
            {
                return (input == DBNull.Value);
            }
            return true;
        }

        public static bool IsNotDbNullOrNull(this object input) => !input.IsDbNullOrNull();

        public static string SerializeToJSON(this NameValueCollection nv)
        {
            if (nv == null)
            {
                ((NameValueCollection)null).SerializeToJSON();
            }
            return nv.ToDictionary().SerializeToJSON();
        }

        public static string SerializeToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string SerializeToString(this object input)
        {
            string str;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, input);
                str = Convert.ToBase64String(stream.ToArray());
                stream.Flush();
            }
            return str;
        }

        /// <summary>        
        /// 时间戳转为C#格式时间  精度秒 
        /// </summary>        
        /// <param name="timeStamp"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>        
        public static DateTime CStampToDateTime(this object timeStamp, DateTime? startTime = null)
        {
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(startTime ?? new DateTime(1970, 1, 1));
            return dtStart.Add(new TimeSpan(timeStamp.CLong(0,true) * 10000000));
        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式   精度秒
        /// </summary>  
        /// <param name="time">时间</param>
        /// <param name="startTime"></param>
        /// <returns>long</returns>  
        public static long CDateTimeToStamp(this DateTime time, DateTime? startTime = null)
        {
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(startTime ?? new DateTime(1970, 1, 1));
            var t = (time.Ticks - dtStart.Ticks) / 10000000;
            return t;
        }
    }

}
