using NUnit.Framework;

namespace RPoney.Framework.Test
{
    [TestFixture]
    public class DesEncryptHelperTest
    {
        [Test]
        public void Encrypt()
        {
            var text = "rain我们｛｝（）++++";
            var encrypt = text.Encrypt(2);
            var decrypt = encrypt.Decrypt(2);
            Assert.AreEqual(text,decrypt);
        }
    }
}
