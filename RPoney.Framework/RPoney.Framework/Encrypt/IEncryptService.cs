namespace RPoney.Encrypt
{
    /// <summary>
    /// 加密算法
    /// </summary>
    public interface IEncryptService
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceTextBytes"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] sourceTextBytes, byte[] key);
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptTextBytes"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] encryptTextBytes, byte[] key);
    }
}
