using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RPoney.Utilty.Http
{
    /// <summary>
    /// Get 请求处理
    /// </summary>
    public static class Get
    {
        #region 同步方法

        /// <summary>
        /// GET方式请求URL，并返回T类型
        /// </summary>
        /// <typeparam name="T">接收JSON的数据类型</typeparam>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="maxJsonLength">允许最大JSON长度</param>
        /// <returns></returns>
        public static T GetJson<T>(string url, Encoding encoding = null, int? maxJsonLength = null)
        {
            var returnText = RequestHelper.HttpGet(url, encoding);
            try
            {
                return returnText.DeserializeFromJSON<T>();
            }
            catch (Exception ex)
            {
                Log.LoggerManager.Error("RPoney.Utilty.Http.Get", $"请求地址：{url}异常", ex);
                return default(T);
            }

        }

        /// <summary>
        /// 从Url下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        public static void Download(string url, Stream stream)
        {
            var wc = new WebClient();
            var data = wc.DownloadData(url);
            stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// 从Url下载，并保存到指定目录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static string Download(string url, string dir)
        {
            Directory.CreateDirectory(dir);
            var httpClient = new HttpClient();
            using (var responseMessage = httpClient.GetAsync(url).Result)
            {
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var fullName = Path.Combine(dir, responseMessage.Content.Headers.ContentDisposition.FileName.Trim('"'));
                    using (var fs = File.Open(fullName, FileMode.Create))
                    {
                        using (var responseStream = responseMessage.Content.ReadAsStreamAsync().Result)
                        {
                            responseStream.CopyTo(fs);
                            return fullName;
                        }
                    }
                }
            }
            return null;
        }
        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】异步GetJson
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="maxJsonLength">允许最大JSON长度</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ErrorJsonResultException"></exception>
        public static async Task<T> GetJsonAsync<T>(string url, Encoding encoding = null, int? maxJsonLength = null)
        {
            string returnText = await RequestHelper.HttpGetAsync(url, encoding);
            try
            {
                return returnText.DeserializeFromJSON<T>();
            }
            catch (Exception ex)
            {
                Log.LoggerManager.Error("RPoney.Utilty.Http.Get", $"请求地址：{url}异常", ex);
                return default(T);
            }
        }

        /// <summary>
        /// 【异步方法】异步从Url下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task DownloadAsync(string url, Stream stream)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            WebClient wc = new WebClient();
            var data = await wc.DownloadDataTaskAsync(url);
            await stream.WriteAsync(data, 0, data.Length);
            //foreach (var b in data)
            //{
            //    stream.WriteAsync(b);
            //}
        }

        /// <summary>
        /// 【异步方法】从Url下载，并保存到指定目录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static async Task<string> DownloadAsync(string url, string dir)
        {
            Directory.CreateDirectory(dir);
            System.Net.Http.HttpClient httpClient = new HttpClient();
            using (var responseMessage = await httpClient.GetAsync(url))
            {
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var fullName = Path.Combine(dir, responseMessage.Content.Headers.ContentDisposition.FileName.Trim('"'));
                    using (var fs = File.Open(fullName, FileMode.Create))
                    {
                        using (var responseStream = await responseMessage.Content.ReadAsStreamAsync())
                        {
                            await responseStream.CopyToAsync(fs);
                            return fullName;
                        }
                    }
                }
            }
            return null;
        }
        #endregion


    }
}
