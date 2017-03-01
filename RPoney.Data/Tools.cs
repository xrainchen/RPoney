using System;
using System.Collections.Generic;
using System.IO;
using RPoney.Data.Pri;

namespace RPoney.Data
{
    public static class Tools
    {
        private static Dictionary<string, string> _connstringTmp = new Dictionary<string, string>();
        public static string ReadConnString(string fileFullName)
        {
            if (string.IsNullOrEmpty(fileFullName))
            {
                throw new Exception("请指定dbc文件");
            }
            string str = "";
            if (!File.Exists(fileFullName))
            {
                throw new Exception("不存在配置文件[" + fileFullName + "]");
            }
            string str2 = File.GetLastWriteTime(fileFullName).ToFileTime().ToString();
            if (_connstringTmp.ContainsKey(fileFullName))
            {
                str = _connstringTmp[fileFullName];
                if (!str.StartsWith(str2 + ","))
                {
                    str = "";
                }
                else
                {
                    str = str.Substring(str2.Length + 1);
                }
            }
            if (str == "")
            {
                str = ConnEncrypt.DecryptFile(fileFullName);
                _connstringTmp[fileFullName] = str2 + "," + str;
            }
            return str;
        }
    }
}
