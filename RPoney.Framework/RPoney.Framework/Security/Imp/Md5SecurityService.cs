using System.Security.Cryptography;

namespace RPoney.Security.Imp
{
    /// <summary>
    /// Md5安全服务
    /// </summary>
    public class Md5SecurityService : ISecurityService
    {
        /// <summary>
        /// MD5加密算法
        /// </summary>
        MD5CryptoServiceProvider md5;
        public Md5SecurityService()
        {
            md5 = new MD5CryptoServiceProvider();
        }
        public byte[] EncryToBytes(byte[] input)
        {
            return md5.ComputeHash(input);
        }

        public string EncryToHexString(byte[] input)
        {
            return EncryToBytes(input).GetHexString();
        }

        public string EncryToLowerHexString(byte[] input)
        {
            return EncryToBytes(input).GetLowerHexString();
        }
    }
}
