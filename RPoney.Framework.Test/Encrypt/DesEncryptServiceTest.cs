using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;
using RPoney.Encrypt.Imp;

namespace RPoney.Framework.Test.Encrypt
{
    [TestFixture]
    public class DesEncryptServiceTest
    {
        [Test]
        public void EncryptServiceTest()
        {
            try
            {
                var desEncryptService = new DesEncryptService();
                var sourceText = Encoding.UTF8.GetBytes("wxf1e4f9f901ad4021").GetHexString();
                var key = EncryptKeyToDecryptKeyBytes("!@#$%");
                var encryptText = desEncryptService.Encrypt(sourceText.GetBytes(), key);
                var decriptText = desEncryptService.Decrypt(encryptText, key);
                Assert.AreEqual(sourceText, decriptText.GetHexString());
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// 加密秘钥转成解密秘钥(单向转)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string EncryptKeyToDecryptKey(string key)
        {
            return EncryptKeyToDecryptKeyBytes(key).Aggregate(string.Empty, (current, b) => current + (char)b);
        }

        /// <summary>
        /// 加密秘钥转成解密秘钥byte[]数组(单向转)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] EncryptKeyToDecryptKeyBytes(string key)
        {
            using (var md5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                var data = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(key), 0, key.Length);
                var builder = new StringBuilder();
                foreach (var t in data)
                {
                    builder.Append(t.ToString("x2"));
                }
                return Encoding.UTF8.GetBytes(builder.ToString().Substring(0, 8));
            }
        }
        private byte[] StringToBytes(string hexString)
        {
            var inputByteArray = new byte[hexString.Length / 2];
            for (var x = 0; x < hexString.Length / 2; x++)
            {
                var i = (Convert.ToInt32(hexString.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            return inputByteArray;
        }
        public string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                returnStr = bytes.Aggregate(returnStr, (current, t) => current + t.ToString("X2"));
            }
            return returnStr;
        }
    }
}
