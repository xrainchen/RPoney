using System;
using System.Collections.Generic;
using System.Text;
using RPoney.Encrypt;
using RPoney.Encrypt.Imp;

namespace RPoney
{
    public static class DesEncryptHelper
    {
        private static readonly IEncryptService encrypt = new EncryptServiceFactory().CreateSecurityService(PublicEnum.EncryptServiceEnum.DES);

        private static readonly Dictionary<int, string> encryptDic =
            new Dictionary<int, string>
            {
                {0,"7b67b8f1" },
                {1,"108cb41d" },
                {2,"6fb6c1b4" },
                {3,"bc8068d5" },
                {4,"e62759e5" },
                {5,"ed27fe22" },
                {6,"0f748a32" },
                {7,"3f16f6f2" }
            };

        public static string Encrypt(this string text, int r)
        {
            try
            {
                return
                    Convert.ToBase64String(encrypt.Encrypt(Encoding.UTF8.GetBytes(text), Encoding.UTF8.GetBytes(encryptDic[r])));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string Decrypt(this string text, int r)
        {
            try
            {
                return Encoding.UTF8.GetString(
                    encrypt.Decrypt(Convert.FromBase64String(text), Encoding.UTF8.GetBytes(encryptDic[r]))
                    );
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
