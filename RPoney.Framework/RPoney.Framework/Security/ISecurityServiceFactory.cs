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
        ISecurityService CreateSecurityService(SecurityServiceEnum securityService);
    }
    /// <summary>
    /// 安全服务类型
    /// </summary>
    public enum SecurityServiceEnum
    {
        /// <summary>
        /// MD5算法
        /// </summary>
        MD5 = 1,
        /// <summary>
        /// SHA1算法
        /// </summary>
        SHA1 = 2
    }
}
