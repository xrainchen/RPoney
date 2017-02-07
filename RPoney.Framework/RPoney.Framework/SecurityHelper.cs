using System.Security.Cryptography;

namespace RPoney
{
    /// <summary>
    /// 安全算法帮助类
    /// </summary>
    public static class SecurityHelper
    {

        /// <summary>
        /// 加密成MD5哈希字节数组
        /// </summary>
        /// <param name="input">源字节数组</param>
        /// <returns>MD5加密的哈希字节数组</returns>
        public static byte[] EncryMd5Bytes(this byte[] input)
        {
            return new MD5CryptoServiceProvider().ComputeHash(input);
        }

        /// <summary>
        /// 加密成SHA1哈希字节数组
        /// </summary>
        /// <param name="input">源字节数组</param>
        /// <returns>SHA1加密的哈希字节数组</returns>
        public static byte[] EncrySha1Bytes(this byte[] input)
        {
            return new SHA1CryptoServiceProvider().ComputeHash(input);
        }
        /// <summary>
        /// 加密成SHA1哈希字节数组
        /// </summary>
        /// <param name="input">源字节数组</param>
        /// <returns>SHA1加密16进制字符串</returns>
        public static string EncrySha1String(this byte[] input)
        {
            return new SHA1CryptoServiceProvider().ComputeHash(input).GetHexString();
        }
    }
}
