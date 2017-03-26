namespace RPoney.Encrypt
{
    /// <summary>
    /// 加密算法工厂
    /// </summary>
    public interface IEncryptServiceFactory
    {
        /// <summary>
        /// 创建加密算法
        /// </summary>
        /// <param name="securityService"></param>
        /// <returns></returns>
        IEncryptService CreateSecurityService(PublicEnum.EncryptServiceEnum securityService);
    }
}
