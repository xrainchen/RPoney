using System;
using System.Collections.Generic;

namespace RPoney.Data.PriClient
{
    public class ConnStringConfig
    {
        // Methods
        public ConnStringConfig()
        {
        }

        public ConnStringConfig(string dataSource, string initialCatalog, string userID, string password, TimeSpan connectionTimeOut)
        {
            this.DataSource = dataSource;
            this.InitialCatalog = initialCatalog;
            this.UserID = userID;
            this.Password = password;
            this.ConnectionTimeOut = connectionTimeOut;
        }

        public static ConnStringConfig FromConnString(string connString)
        {
            try
            {
                string[] strArray = connString.Split(new char[] { ';' });
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (string str in strArray)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        string[] strArray2 = str.Split(new char[] { '=' });
                        if (strArray2.Length == 2)
                        {
                            dic[strArray2[0].ToLower().Replace(" ", "")] = strArray2[1];
                        }
                    }
                }
                ConnStringConfig config = new ConnStringConfig
                {
                    DataSource = ReadConfig(dic, new string[] {
                    "Data Source",
                    "Server",
                    "Address",
                    "Addr",
                    "Network Address"
                }),
                    InitialCatalog = ReadConfig(dic, new string[] {
                    "initial catalog",
                    "Database"
                }),
                    UserID = ReadConfig(dic, new string[] {
                    "User ID",
                    "uid"
                }),
                    Password = ReadConfig(dic, new string[] {
                    "Password",
                    "pwd"
                })
                };
                string str2 = ReadConfig(dic, new string[] { "Connect Timeout", "Connection Timeout" });
                if (!string.IsNullOrEmpty(str2))
                {
                    int result = 0;
                    int.TryParse(str2, out result);
                    if (result > 0)
                    {
                        config.ConnectionTimeOut = TimeSpan.FromSeconds((double)result);
                    }
                }
                return config;
            }
            catch
            {
                return null;
            }
        }

        private static string ReadConfig(Dictionary<string, string> dic, params string[] keys)
        {
            foreach (string str in keys)
            {
                string key = str.ToLower().Replace(" ", "");
                if (dic.ContainsKey(key))
                {
                    return UrlDecoding(dic[key]);
                }
            }
            return "";
        }

        public override string ToString()
        {
            string format = "data source={0};initial catalog={1};user id={2};password={3};persist security info=True;packet size=4096;connection reset=false;min pool size=1;Max Pool Size = 512;";
            format = string.Format(format, new object[] { UrlEncoding(this.DataSource), UrlEncoding(this.InitialCatalog), UrlEncoding(this.UserID), UrlEncoding(this.Password) });
            if (this.ConnectionTimeOut > TimeSpan.Zero)
            {
                format = format + "Connection Timeout=" + this.ConnectionTimeOut.TotalSeconds.ToString() + ";";
            }
            return format;
        }

        private static string UrlDecoding(string input) =>
            input;

        private static string UrlEncoding(string input) =>
            input;
 
    // Properties
        public TimeSpan ConnectionTimeOut { get; set; }

        public string DataSource { get; set; }

        public string InitialCatalog { get; set; }

        public string Password { get; set; }

        public string UserID { get; set; }
    }

}
