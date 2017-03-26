namespace RPoney.Security
{
    /// <summary>
    /// 安全服务工厂
    /// </summary>
    public interface ISecurityServiceFactory
    {
        /// <summary>
        /// 创建一个安全服务
        /// </summary>
        /// <param name="securityService"></param>
        /// <returns></returns>
        ISecurityService CreateSecurityService(PublicEnum.SecurityServiceEnum securityService);
    }
}
