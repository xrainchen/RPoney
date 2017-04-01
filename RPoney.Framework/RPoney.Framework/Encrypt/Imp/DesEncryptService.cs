using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RPoney.Encrypt.Imp
{
    /// <summary>
    /// DES算法
    /// </summary>
    public class DesEncryptService : IEncryptService
    {
        public byte[] Encrypt(byte[] encryptTextBytes, byte[] key)
        {
            //进行一次MD5加密处理，取得8位长度，作为真实的密钥
            
            using (var desCryptoServiceProvider = new DESCryptoServiceProvider())
            {
                desCryptoServiceProvider.Key = desCryptoServiceProvider.IV = key;
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, desCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptTextBytes, 0, encryptTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
        public byte[] Decrypt(byte[] sourceTextBytes, byte[] key)
        {
            //传入的解密密钥是md5值，而不是真正的定义的密钥
            using (var desCryptoServiceProvider = new DESCryptoServiceProvider())
            {
                desCryptoServiceProvider.Key = key;
                desCryptoServiceProvider.IV = key;
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, desCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(sourceTextBytes, 0, sourceTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }


    }
}
