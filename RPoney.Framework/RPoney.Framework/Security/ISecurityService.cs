namespace RPoney.Security
{
    /// <summary>
    /// 一个抽象的安全服务接口
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// 加密成字符串
        /// </summary>
        /// <param name="input">输入字节</param>
        /// <returns>加密后字节</returns>
        byte[] EncryToBytes(byte[] input);
        /// <summary>
        /// 加密成16进制字符串
        /// </summary>
        /// <param name="input">输入字节</param>
        /// <returns>加密后16进制字符串</returns>
        string EncryToHexString(byte[] input);

        /// <summary>
        /// 加密成小写16进制字符串
        /// </summary>
        /// <param name="input">输入字节</param>
        /// <returns>加密后小写16进制字符串</returns>
        string EncryToLowerHexString(byte[] input);
    }
}
