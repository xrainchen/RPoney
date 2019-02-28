using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace RPoney.Utilty
{

    /// <summary>
    /// Resultful返回结果模型
    /// </summary>
    public class RestfulResultModel
    {
        /// <summary>
        /// 响应头
        /// </summary>
        public IDictionary<string, string> ReponseHeaders { get; set; }
        /// <summary>
        /// 响应文本
        /// </summary>
        public string ReponseContent { get; set; }
        /// <summary>
        /// 请求状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// 请求状态码描述
        /// </summary>
        public string StatusDescription { get; set; }

        public string GetReponseHeaderValue(string reponseHeaderName)
        {
            if (ReponseHeaders == null || !ReponseHeaders.ContainsKey(reponseHeaderName))
            {
                return string.Empty;
            }
            return ReponseHeaders[reponseHeaderName];
        }
    }
    /// <summary>
    /// Restful帮助类
    /// </summary>
    public class RestfulHelper
    {
        public static RestfulResultModel Head(string url, Dictionary<string, string> head, int timeout = 0)
        {
            return Request(url, head, "HEAD", timeout);
        }
        public static RestfulResultModel Get(string url, Dictionary<string, string> head, int timeout = 0)
        {
            return Request(url,head, "GET", timeout);
        }
        public static RestfulResultModel Post(string url, Dictionary<string, string> head, Dictionary<string, string> body, string contentType = "application/x-www-form-urlencoded", int timeout = 0)
        {
            return Request(url, head, body, "POST", contentType,timeout);
        }
        public static RestfulResultModel Put(string url, Dictionary<string, string> head, Dictionary<string, string> body, string contentType = "application/x-www-form-urlencoded", int timeout = 0)
        {
            return Request(url, head, body, "PUT", contentType, timeout);
        }
        public static RestfulResultModel PutFile(string url, Dictionary<string, string> head, Stream stream, string contentType = "application/x-www-form-urlencoded", int timeout = 0)
        {
            return RequestUploadFile(url, head, "PUT", stream, timeout);
        }
        public static RestfulResultModel Delete(string url, Dictionary<string, string> head, Dictionary<string, string> body, string contentType = "application/x-www-form-urlencoded", int timeout = 0)
        {
            return Request(url,head,body, "DELETE", contentType, timeout);
        }

        public static RestfulResultModel Request(string url, Dictionary<string, string> head,Dictionary<string, string> body, string method,string contentType= "application/x-www-form-urlencoded", int timeout = 0)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null) return null;
            request.Method = method;
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }
            if (head != null)
            {
                foreach (var h in head)
                {
                    request.Headers.Add(h.Key, h.Value);
                }
            }
            byte[] bytes = Encoding.UTF8.GetBytes(Encode(body));
            request.ContentType = contentType;
            request.ContentLength = bytes.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (null == response)
                    {
                        return null;
                    }
                    var result = new RestfulResultModel()
                    {
                        StatusCode = response.StatusCode,
                        StatusDescription = response.StatusDescription,
                        ReponseHeaders = response.Headers.ToDictionary()
                    };
                    using (Stream reponseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(reponseStream, Encoding.UTF8))
                        {
                            result.ReponseContent = reader.ReadToEnd();
                        }
                    }
                    return result;
                }
            }
        }

        public static RestfulResultModel Request(string url, Dictionary<string, string> head, string body, string method, string contentType = "application/x-www-form-urlencoded", int timeout = 0)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null) return null;
            request.Method = method;
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }
            if (head != null)
            {
                foreach (var h in head)
                {
                    request.Headers.Add(h.Key, h.Value);
                }
            }
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            request.ContentType = contentType;
            request.ContentLength = bytes.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (null == response)
                    {
                        return null;
                    }
                    var result = new RestfulResultModel()
                    {
                        StatusCode = response.StatusCode,
                        StatusDescription = response.StatusDescription,
                        ReponseHeaders = response.Headers.ToDictionary()
                    };
                    using (Stream reponseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(reponseStream, Encoding.UTF8))
                        {
                            result.ReponseContent = reader.ReadToEnd();
                        }
                    }
                    return result;
                }
            }
        }
        public static RestfulResultModel Request(string url, Dictionary<string, string> head, string method, int timeout = 0)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null) return null;
            request.Method = method;
            if (head != null)
            {
                foreach (var h in head)
                {
                    request.Headers.Add(h.Key, h.Value);
                }
            }
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (null == response)
                {
                    return null;
                }
                var result = new RestfulResultModel()
                {
                    StatusCode = response.StatusCode,
                    StatusDescription = response.StatusDescription,
                    ReponseHeaders = response.Headers.ToDictionary()
                };
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result.ReponseContent = reader.ReadToEnd();
                    }
                }
                return result;
            }
        }
      
        public static RestfulResultModel RequestUploadFile(string url, Dictionary<string, string> head, string method, Stream stream,int timeout=0)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "multipart/form-data";
            request.ContentLength = stream.Length;
            if (timeout != 0)
            {
                request.Timeout = timeout;
            }
            if (head != null)
            {
                foreach (var h in head)
                {
                    request.Headers.Add(h.Key, h.Value);
                }
            }
            if (stream.Length > 0)
            {
                stream.Position = 0;
                //直接写入流
                var requestStream = request.GetRequestStream();
                var buffer = new byte[1024];//1kb
                var bytesRead = 0;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }
                stream.Close();//关闭文件访问
            }
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (null == response)
                {
                    return null;
                }
                var result = new RestfulResultModel()
                {
                    StatusCode = response.StatusCode,
                    StatusDescription = response.StatusDescription,
                    ReponseHeaders = response.Headers.ToDictionary()
                };
                using (Stream reponseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(reponseStream, Encoding.UTF8))
                    {
                        result.ReponseContent = reader.ReadToEnd();
                    }
                }
                return result;
            }
        }
        public static string Encode(Dictionary<string, string> data)
        {
            if (data == null) return string.Empty;
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in data)
            {
                builder.AppendFormat("{0}={1}&", pair.Key, HttpUtility.UrlEncode(pair.Value));
            }
            char[] trimChars = new char[] { '&' };
            return builder.ToString().TrimEnd(trimChars);
        }
    }
}
