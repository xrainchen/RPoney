using System.Collections.Generic;
using System.Linq;

namespace RPoney.Utilty
{
    public class UrlTool
    {
        /// <summary>
        /// 替换URL参数值
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static string ReplaceUrlParamValue(string url, string paramName, string paramValue)
        {
            if (string.IsNullOrWhiteSpace(url)) return url;
            //参数和参数名为空的话就返回原来的URL
            if (string.IsNullOrWhiteSpace(paramValue) || string.IsNullOrWhiteSpace(paramName))
            {
                return url;
            }
            var urlSplit = url.Split('?');
            if (urlSplit.Length == 1)
            {
                return $"{url}?{paramName}={paramValue}";
            }
            var urlPre = urlSplit[0];
            var urlAfter = urlSplit[1];
            var urlAfterList = urlAfter.Split('&');
            var urlAfterDic = new Dictionary<string, string>();
            if (urlAfterList.Length > 0)
            {
                foreach (var item in urlAfterList)
                {
                    var itemKeyPair = item.Split('=');
                    var itemName = itemKeyPair[0].ToLower();
                    if (!urlAfterDic.ContainsKey(itemName) && !string.IsNullOrWhiteSpace(itemName))
                    {
                        var itemValue = itemKeyPair.Length > 1 ? itemKeyPair[1] : "";
                        urlAfterDic[itemName] = itemValue;
                    }
                }
                if (!urlAfterDic.ContainsKey(paramName.ToLower()))
                {
                    urlAfterDic[paramName.ToLower()] = paramValue;
                }
            }
            return urlPre + "?" + urlAfterDic.Aggregate(string.Empty, (current, dic) => current + $"{dic.Key}={dic.Value}&");
        }
    }
}
