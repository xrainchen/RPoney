﻿using System;
using System.IO;
using System.Net;
using System.Text;
using RPoney.HttpTools.Model;

namespace RPoney.HttpTools
{
    public class HttpService
    {
        public string GetResult(RequestHeaderModel model)
        {
            switch (model.Method.ToLower())
            {
                case "get":
                    return HttpHelper.Get(model.Url, Encoding.GetEncoding(model.Charset), GetUserAgent(model.UserAgent));
                case "post":
                    return HttpHelper.Post(model.Url, Encoding.GetEncoding(model.Charset), GetUserAgent(model.UserAgent), model.Param, model.ContentType, model.FileStream);
                default:
                    return string.Empty;
            }
        }

        private string GetUserAgent(string userAgentType)
        {
            switch (userAgentType.ToLower())
            {
                case "windows":
                    return "Mozilla/5.0 (Windows; U; Windows NT 5.2) AppleWebKit/525.13 (KHTML, like Gecko) Chrome/0.2.149.27 Safari/525.13 ";
                case "android":
                    return "Mozilla/5.0 (Linux; U; Android 4.0.3; zh-cn; M032 Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";
                case "ios":
                    return "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";
                default:
                    return userAgentType;
            }
        }
    }

    public static class HttpHelper
    {
        #region 代理

        private static WebProxy _webproxy = null;

        /// <summary>
        /// 设置Web代理
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void SetHttpProxy(string host, string port, string username, string password)
        {
            var cred = new NetworkCredential(username, password);
            if (!string.IsNullOrEmpty(host))
            {
                _webproxy = new WebProxy(host + ":" + port ?? "80", true, null, cred);
            }
        }

        /// <summary>
        /// 清除Web代理状态
        /// </summary>
        public static void RemoveHttpProxy()
        {
            _webproxy = null;
        }

        #endregion
        private static readonly int _timeOut = 10000;
        private const string MethodGet = "GET";
        private const string MethodPost = "POST";
        private const string RequestAccept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        public static string Get(string url, Encoding encoding, string userAgent)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = MethodGet;
            request.Timeout = _timeOut;
            request.Proxy = _webproxy;
            request.UserAgent = userAgent;
            var response = (HttpWebResponse)request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                using (var myStreamReader = new StreamReader(responseStream, encoding))
                {
                    return myStreamReader.ReadToEnd();
                }
            }
        }

        public static string Post(string url, Encoding encoding, string userAgent, string requestData, string contentType, Stream fileStream)
        {
            var stream = new MemoryStream();
            var postDataBytes = string.IsNullOrWhiteSpace(requestData) ? new byte[0] : encoding.GetBytes(requestData);
            stream.Write(postDataBytes, 0, postDataBytes.Length);
            if (null != fileStream)
            {
                //写入文件
                var buffer = new byte[1024];
                var bytesRead = 0;
                fileStream.Position = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    stream.Write(buffer, 0, bytesRead);
                }
            }
            return Post(url, encoding, userAgent, stream, contentType);
        }
        public static string Post(string url, Encoding encoding, string userAgent, Stream stream, string contentType)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = MethodPost;
            request.Timeout = _timeOut;
            request.Proxy = _webproxy;
            request.UserAgent = userAgent;
            request.ContentType = contentType;
            request.ContentLength = stream.Length;
            request.Accept = RequestAccept;
            request.KeepAlive = true;
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
            var response = (HttpWebResponse)request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                using (var myStreamReader = new StreamReader(responseStream, string.IsNullOrWhiteSpace(response.CharacterSet) ? encoding : Encoding.GetEncoding(response.CharacterSet)))
                {
                    var retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

        /// <summary>
        /// 获取文件上传流
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="chartSet"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static Stream GetUploadFileStream(Stream fileStream, string chartSet, string fileName, ref string contentType)
        {
            if (contentType == null) throw new ArgumentNullException(nameof(contentType));
            var postStream = new MemoryStream();
            var boundary = DateTime.Now.Ticks.ToString("x");
            var itemBoundaryBytes = Encoding.GetEncoding(chartSet).GetBytes("\r\n--" + boundary + "\r\n");
            var endBoundaryBytes = Encoding.GetEncoding(chartSet).GetBytes("\r\n--" + boundary + "--\r\n");
            //请求头部信息
            var sbHeader = $"Content-Disposition:form-data;name=\"media\";filename=\"{Path.GetFileName(fileName)}\"\r\nContent-Type:application/octet-stream\r\n\r\n";
            var postHeaderBytes = Encoding.GetEncoding(chartSet).GetBytes(sbHeader);
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            var buffer = new byte[1024];
            var bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                postStream.Write(buffer, 0, bytesRead);
            }
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            contentType = $"multipart/form-data; boundary={boundary}";
            return postStream;
        }
    }
}
