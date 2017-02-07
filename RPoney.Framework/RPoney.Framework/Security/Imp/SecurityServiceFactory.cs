using System;

namespace RPoney.Security.Imp
{
    /// <summary>
    /// 安全服务工厂 
    /// </summary>
    public class SecurityServiceFactory : ISecurityServiceFactory
    {
        public ISecurityService CreateSecurityService(SecurityServiceEnum securityService)
        {
            switch (securityService)
            {
                case SecurityServiceEnum.MD5:
                    return new Md5SecurityService();
                case SecurityServiceEnum.SHA1:
                    return new Sha1SecurityService();
                default:
                    throw new Exception($"SecurityServiceEnum类型不存在,securityService:{(int)securityService}");
            }
        }
    }
}
