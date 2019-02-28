using System.Web;

namespace RPoney.Utilty
{
    public static class BrowserHelper
    {
        /// <summary>
        /// 判断是否在微信内置浏览器中
        /// </summary>
        /// <param name="httpContext">HttpContextBase对象</param>
        /// <returns>true：在微信内置浏览器内。false：不在微信内置浏览器内。</returns>
        public static bool SideInWeixinBrowser(this HttpContextBase httpContext)
        {
            var userAgent = httpContext.Request.UserAgent;
            return userAgent != null&& (userAgent.Contains("MicroMessenger") || userAgent.Contains("Windows Phone"));
        }
    }
}
