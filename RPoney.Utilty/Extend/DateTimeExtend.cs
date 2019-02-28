using System;

namespace RPoney.Utilty.Extend
{
    public static class DateTimeExtend
    {
        /// <summary>
        /// 获取给定时间的本地时间戳（1970秒级）
        /// </summary>
        /// <param name="dateTime">要转换的时间</param>
        /// <returns></returns>
        public static long ToLocalTimeStamp(this DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.Subtract(TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds);
        }
        public static long ToLocalMilliTimeStamp(this DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.Subtract(TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalMilliseconds);
        }
        /// <summary>
        /// 从时间戳获取时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromTimeStamp(long timeStamp)
        {

            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).Add(TimeSpan.FromSeconds(timeStamp));
        }
        public static DateTime GetDateTimeFromMilliTimeStamp(long timeStamp)
        {

            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).Add(TimeSpan.FromMilliseconds(timeStamp));
        }
        /// <summary>
        /// 20171105210318000+0800
        /// </summary>
        /// <param name="utc">2017-11-05 21:03:18:000+0800</param>
        /// <param name="defaultTime"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromUtc(this string utc, DateTime defaultTime)
        {
            try
            {
                var time = utc.Split('+')[0];
                var year = time.Substring(0, 4);
                var month = time.Substring(4, 2);
                var day = time.Substring(6, 2);
                var hour = time.Substring(8, 2);
                var minute = time.Substring(10, 2);
                var second = time.Substring(12, 2);
                var milliSecond = time.Substring(13, 3);
                return new DateTime(
                    year.CInt(0, false),
                    month.CInt(0, false),
                    day.CInt(0, false),
                    hour.CInt(0, false),
                    minute.CInt(0, false),
                    second.CInt(0, false),
                     milliSecond.CInt(0, false)
                    );
            }
            catch
            {
                return defaultTime;
            }
        }
    }
}
