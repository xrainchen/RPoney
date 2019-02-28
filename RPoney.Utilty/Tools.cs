using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using RPoney.Utilty.Extend;

namespace RPoney.Utilty
{
    public static class Tools
    {
        /// <summary>
        /// 取客户端Ip
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            if (HttpContext.Current == null) return "";

            var request = HttpContext.Current.Request;

            try
            {
                string returnValue = "";
                if (request.ServerVariables["HTTP_VIA"] != null)
                {
                    returnValue = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (string.IsNullOrEmpty(returnValue))
                {
                    if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                        returnValue = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                if (string.IsNullOrEmpty(returnValue))
                {
                    returnValue = request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrEmpty(returnValue))
                {
                    returnValue = HttpContext.Current.Request.UserHostAddress;
                }
                return returnValue;
            }
            catch
            {
                try
                {
                    return request.UserHostAddress;
                }
                catch { return "0.0.0.0"; }
            }
        }

        /// <summary>
        /// 根据指定的字符串产生指定长度的随机字符串
        /// </summary>
        /// <param name="sourceString">指定的字符串</param>
        /// <param name="length">产生的随机字符串的长度</param>
        /// <param name="seed">随机数种子，并发使用时必须传入不同的种子以避免生成重复的随机字符串</param>
        /// <returns></returns>
        public static string GetRandom(string sourceString, int length, int seed = 0)
        {
            var output = string.Empty;
            var arr = sourceString.ToCharArray();
            var ran = seed == 0 ? new Random() : new Random(seed);
            while (output.Length < length)
            {
                output += arr[ran.Next(0, arr.Length)];
            }

            return output;
        }

        /// <summary>
        /// 原子基数
        /// </summary>
        private static int interLockedSource = 100000;//百万
        /// <summary>
        /// 创建订单号(16位)  -10位时间戳+百万级自增
        /// </summary>
        /// <returns></returns>
        public static string CreateOrderNo()
        {
            Interlocked.Increment(ref interLockedSource);
            if (interLockedSource >= 1000000)//千万重置
            {
                interLockedSource = 100000;
                Interlocked.Increment(ref interLockedSource);
            }
            return $"{DateTime.Now.ToLocalTimeStamp()}{interLockedSource}";
        }

        /// <summary>
        /// 手机号校验
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(this string phone, string regex = "")
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            if (string.IsNullOrWhiteSpace(regex)) regex = "^1[0-9]{10}$";
            return new Regex(regex).IsMatch(phone);
        }
    }
}
