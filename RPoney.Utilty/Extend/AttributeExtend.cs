using System;

namespace RPoney.Utilty.Extend
{
    /// <summary>
    /// 自定义枚举备注属性
    /// </summary>
    public class RemarkAttribute : Attribute
    {
        public string Remark { get; set; }

        public RemarkAttribute(string remark)
        {
            Remark = remark;
        }
    }
    /// <summary>
    /// 全局特性
    /// </summary>
    public class GlobalSettingAttribute : Attribute
    {
        public string GlobalSetting { get; set; }

        public GlobalSettingAttribute(string setting)
        {
            GlobalSetting = setting;
        }
    }
    /// <summary>
    /// URL特性
    /// </summary>
    public class UrlAttribute : Attribute
    {
        public string Url { get; set; }

        public UrlAttribute(string url)
        {
            Url = url;
        }
    }
}
