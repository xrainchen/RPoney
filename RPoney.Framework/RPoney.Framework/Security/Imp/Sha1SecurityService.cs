using System.Security.Cryptography;

namespace RPoney.Security.Imp
{
    /// <summary>
    /// SHA1安全服务
    /// </summary>
    public class Sha1SecurityService : ISecurityService
    {
        /// <summary>
        /// SHA1加密算法
        /// </summary>
        SHA1CryptoServiceProvider sha1;
        public Sha1SecurityService()
        {
            sha1 = new SHA1CryptoServiceProvider();
        }
        public byte[] EncryToBytes(byte[] input)
        {
            return sha1.ComputeHash(input);
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
