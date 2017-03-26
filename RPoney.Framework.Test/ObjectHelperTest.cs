using System;
using NUnit.Framework;

namespace RPoney.Framework.Test
{
    [TestFixture]
    public class ObjectHelperTest
    {
        [Test]
        public void CStampToDateTime()
        {
            //1490490649---2017/3/26 9:10:49
            const long timeStamp = 1490490649;
            var time = timeStamp.CStampToDateTime();
            Assert.AreEqual(time, new DateTime(2017, 3, 26, 9, 10, 49));
        }
        [Test]
        public void CDateTimeToStamp()
        {
            //1490490649---2017/3/26 9:10:49
            var dateTime = new DateTime(2017, 3, 26, 9, 10, 49);
            Assert.AreEqual(1490490649, dateTime.CDateTimeToStamp());
        }
    }
}
