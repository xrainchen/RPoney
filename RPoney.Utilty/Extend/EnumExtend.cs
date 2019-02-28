using System;
using System.Collections.Generic;
using System.Linq;

namespace RPoney.Utilty.Extend
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtend
    {
        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetRemark(this Enum em)
        {
            var remark = string.Empty;
            if (em == null) return "";

            var type = em.GetType();
            var fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;

            var attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);

            if (attrs.Length <= 0) return remark;

            foreach (RemarkAttribute attr in attrs)
            {
                remark = attr.Remark;
            }

            return remark;
        }

        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetSettingKey(this Enum em)
        {
            var remark = string.Empty;
            if (em == null) return "";

            var type = em.GetType();
            var fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;

            var attrs = fd.GetCustomAttributes(typeof(GlobalSettingAttribute), false);

            if (attrs.Length <= 0) return remark;

            foreach (GlobalSettingAttribute attr in attrs)
            {
                remark = attr.GlobalSetting;
            }

            return remark;
        }

        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetUrl(this Enum em)
        {
            var url = string.Empty;
            if (em == null) return "";

            var type = em.GetType();
            var fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;

            var attrs = fd.GetCustomAttributes(typeof(UrlAttribute), false);

            if (attrs.Length <= 0) return url;

            foreach (UrlAttribute attr in attrs)
            {
                url = attr.Url;
            }

            return url;
        }

        /// <summary>
        /// 获取枚举的remark列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumRemarks<T>()
        {
            var enumList = new Dictionary<int, string>();

            var t = typeof(T);

            foreach (var e in Enum.GetValues(t))
            {
                var val = (int)e;

                var type = e.GetType();
                var fd = type.GetField(e.ToString());

                var attr = (RemarkAttribute)fd?.GetCustomAttributes(typeof(RemarkAttribute), false).FirstOrDefault();
                var remark = attr?.Remark;
                enumList.Add(val, remark);
            }

            return enumList;
        }
    }
}
