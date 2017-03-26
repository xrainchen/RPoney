using System;

namespace RPoney.Encrypt.Imp
{
    /// <summary>
    /// 加密服务
    /// </summary>
    public class EncryptServiceFactory : IEncryptServiceFactory
    {
        public IEncryptService CreateSecurityService(PublicEnum.EncryptServiceEnum securityService)
        {
            switch (securityService)
            {
                case PublicEnum.EncryptServiceEnum.DES:
                    return new DesEncryptService();
            }
            throw new NotImplementedException();
        }
    }
}
