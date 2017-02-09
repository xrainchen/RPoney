using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Script.Serialization;

namespace RPoney
{
    public static class StringHelper
    {
        public static T DeserializeFromJSON<T>(this string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(jsonString);
        }

        public static object DeserializeObject(this string input)
        {
            IFormatter formatter = new BinaryFormatter();
            byte[] buffer = Convert.FromBase64String(input);
            using (Stream stream = new MemoryStream(buffer, 0, buffer.Length))
            {
                return formatter.Deserialize(stream);
            }
        }

        public static T DeserializeObject<T>(this string input) where T : class, new() =>
            (input.DeserializeObject() as T);

        public static object DeserializeObjectFromJSON(this string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.DeserializeObject(jsonString);
        }

        public static bool IsEmpty(this string input)
        {
            if (input != null)
            {
                return (input.Length == 0);
            }
            return true;
        }
  
        public static string Md5(this string input) => input.Md5(Encoding.UTF8);

        public static string Md5(this string input, Encoding encoding) => encoding.GetBytes(input).EncryMd5String();

        public static string Md5Lower(this string input) => input.Md5Lower(Encoding.UTF8);

        public static string Md5Lower(this string input, Encoding encoding) => encoding.GetBytes(input).EncryLowerMd5String();

        public static string Repeat(this string input, int times)
        {
            if (times < 0)
            {
                throw new Exception("重复次数必须大等于0");
            }
            if (times == 0)
            {
                return string.Empty;
            }
            if (input.IsEmpty() || (times == 1))
            {
                return input;
            }
            int length = input.Length;
            char[] sourceArray = input.ToCharArray();
            char[] destinationArray = new char[length * times];
            for (int i = 0; i < times; i++)
            {
                Array.Copy(sourceArray, 0, destinationArray, i * length, length);
            }
            return new string(destinationArray);
        }

        public static string Sha1(this string input) => input.Sha1(Encoding.UTF8);

        public static string Sha1(this string input, Encoding encoding) => encoding.GetBytes(input).EncrySha1String();

        public static string Sha1Lower(this string input, Encoding encoding) => encoding.GetBytes(input).EncryLowerSha1String();

        public static string TrimEx(this string input)
        {
            return !string.IsNullOrEmpty(input) ? input.Trim() : string.Empty;
        }
    }



}
