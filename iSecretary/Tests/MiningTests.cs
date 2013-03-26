using System.Collections.Generic;
using Data;
using EmailDataMiner;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class MiningTests
    {
        [Test]
        public void CheckCanGetFromEmailAddress()
        {
            List<string> bad;
            var emailAddresses = Extractor.GetEmailAddresses(new Repository().StorageWrapper.Data.EmailExportDirectory, out bad);

            Assert.Greater(0, emailAddresses.Count);
            Assert.AreEqual(0, bad.Count);
        }
    }
}
