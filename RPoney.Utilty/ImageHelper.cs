using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;

namespace RPoney.Utilty
{
    /// <summary>
    ///     图片操作帮助类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        ///     获取网络图片并转为Base64编码
        /// </summary>
        /// <param name="url">网络图片地址</param>
        /// <returns></returns>
        public static string GetUrlImageToBase64(string url)
        {
            var htmlstr = string.Empty;
            try
            {
                byte[] byteData = null;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 30000;
                request.AllowAutoRedirect = true;
                var response = (HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        var photoImage = Image.FromStream(stream);
                        var mStream = new MemoryStream();
                        photoImage.Save(mStream, photoImage.RawFormat); //从图片中获取缓存流
                        byteData = mStream.GetBuffer();
                        mStream.Close();
                    }
                }
                response.Close();
                if (byteData != null && !byteData.IsDbNullOrNull())
                    htmlstr = Convert.ToBase64String(byteData);
            }
            catch (Exception ex)
            {
                throw new Exception("获取图片异常", ex);
            }
            return htmlstr;
        }

        /// <summary>
        ///     生成缩略图
        /// </summary>
        /// <param name="sourceImage">原始图片文件</param>
        /// <param name="quality">质量压缩比</param>
        /// <param name="multiple">收缩倍数</param>
        /// <returns>成功返回true,失败则返回false</returns>
        public static byte[] ZipImage(Image sourceImage, long quality, int multiple)
        {
            try
            {
                var imageQuality = quality;
                var myEncoder = Encoder.Quality;
                var myEncoderParameters = new EncoderParameters(1);
                var myEncoderParameter = new EncoderParameter(myEncoder, imageQuality);
                myEncoderParameters.Param[0] = myEncoderParameter;
                float xWidth = sourceImage.Width;
                float yWidth = sourceImage.Height;
                Image newImage = new Bitmap((int)(xWidth / multiple), (int)(yWidth / multiple));
                var g = Graphics.FromImage(newImage);

                g.DrawImage(sourceImage, 0, 0, xWidth / multiple, yWidth / multiple);
                g.Dispose();

                var byteImage = ImgToByt(newImage, sourceImage.RawFormat);

                return byteImage;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     图片转换成字节流
        /// </summary>
        /// <param name="img">要转换的Image对象</param>
        /// <param name="imgFormat">要转换的Image对象</param>
        /// <returns>转换后返回的字节流</returns>
        private static byte[] ImgToByt(Image img, ImageFormat imgFormat)
        {
            var ms = new MemoryStream();
            img.Save(ms, imgFormat);
            var imagedata = ms.GetBuffer();
            ms.Close();
            return imagedata;
        }

        /// <summary>
        ///     生成文件名称
        /// </summary>
        /// <param name="fileExtendName">文件扩展名称</param>
        /// <returns></returns>
        private static string GetFileName(string fileExtendName)
        {
            var seed = Guid.NewGuid().GetHashCode();
            var rd = new Random(seed);
            var radNum = rd.Next(10000, 99999);
            var fileName = "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" +
                           DateTime.Now.Ticks + radNum + fileExtendName;
            return fileName;
        }


        #region 获取图片扩展名

        private static Dictionary<string, ImageFormat> _imageFormats;

        private static Dictionary<string, ImageFormat> GetImageFormats()
        {
            var dic = new Dictionary<string, ImageFormat>();
            var properties = typeof(ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public);
            foreach (var property in properties)
            {
                var format = property.GetValue(null, null) as ImageFormat;
                if (format == null)
                    continue;
                dic.Add(("." + property.Name).ToLower(), format);
            }
            return dic;
        }

        /// <summary>
        ///     获取所有支持的图片格式字典
        /// </summary>
        public static Dictionary<string, ImageFormat> ImageFormats
            => _imageFormats ?? (_imageFormats = GetImageFormats());

        /// <summary>
        ///     根据图像获取图像的扩展名
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string GetExtension(Image image)
        {
            foreach (var pair in ImageFormats)
                if (pair.Value.Guid == image.RawFormat.Guid)
                    return pair.Key;
            throw new BadImageFormatException();
        }

        /// <summary>
        ///     根据图像ImageFormat获取图像的扩展名
        /// </summary>
        /// <param name="imageformat"></param>
        /// <returns></returns>
        public static string GetExtension(ImageFormat imageformat)
        {
            foreach (var pair in ImageFormats)
                if (pair.Value.Guid == imageformat.Guid)
                    return pair.Key;
            throw new BadImageFormatException();
        }

        /// <summary>
        ///     根据图像获取图像的扩展名的 ImageFormat枚举；
        /// </summary>
        /// <param name="extensionName"></param>
        /// <returns></returns>
        public static ImageFormat GetExtensionImageFormat(string extensionName)
        {
            foreach (var pair in ImageFormats)
                if (pair.Key.ToLower() == extensionName.ToLower())
                    return pair.Value;
            throw new BadImageFormatException();
        }

        #endregion
    }
}
