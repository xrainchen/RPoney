using RPoney.Security;
using RPoney.Security.Imp;

namespace RPoney
{
    /// <summary>
    /// 安全算法帮助类
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// 安全服务工厂
        /// </summary>
        static ISecurityServiceFactory _securityServiceFactory;
        static SecurityHelper()
        {
            _securityServiceFactory = new SecurityServiceFactory();
        }
        /// <summary>
        /// 加密成MD5哈希字节数组
        /// </summary>
        /// <param name="input">源字节数组</param>
        /// <returns>MD5加密的哈希字节数组</returns>
        public static byte[] EncryMd5Bytes(this byte[] input)
        {
            return _securityServiceFactory.CreateSecurityService(PublicEnum.SecurityServiceEnum.MD5).EncryToBytes(input);
        }
        /// <summary>
        /// 加密成MD5哈希字节数组
        /// </summary>
        /// <param name="input">源字节数组</param>
        /// <returns>MD5加密的16进制字符串</returns>
        public static string EncryMd5String(this byte[] input)
        {
            return input.EncryMd5Bytes().GetHexString();
        }
        /// <summary>
        /// 加密成MD5哈希字节数组
        /// </summary>
        /// <param name="input">源字节数组</param>
        /// <returns>MD5加密的16进制字符串</returns>
        public static string EncryLowerMd5String(this byte[] input)
        {
            return input.EncryMd5Bytes().GetLowerHexString();
        }
        /// <summary>
        /// 加密成SHA1哈希字节数组
        /// </summary>
        /// <param name="input">源字节数组</param>
        /// <returns>SHA1加密的哈希字节数组</returns>
        public static byte[] EncrySha1Bytes(this byte[] input)
        {
            return _securityServiceFactory.CreateSecurityService(PublicEnum.SecurityServiceEnum.SHA1).EncryToBytes(input);
        }
        /// <summary>
        /// 加密成SHA1哈希字节数组
        /// </summary>
        /// <param name="input">源字节数组</param>
        /// <returns>SHA1加密16进制字符串</returns>
        public static string EncrySha1String(this byte[] input)
        {
            return input.EncrySha1Bytes().GetHexString();
        }
        /// <summary>
        /// 加密成MD5哈希字节数组
        /// </summary>
        /// <param name="input">源字节数组</param>
        /// <returns>SHA1加密16进制字符串</returns>
        public static string EncryLowerSha1String(this byte[] input)
        {
            return input.EncrySha1Bytes().GetLowerHexString();
        }
    }
}
