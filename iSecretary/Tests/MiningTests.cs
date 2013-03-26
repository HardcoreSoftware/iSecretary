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
            var emailAddresses = Extractor.GetEmailAddresses(new Repository().StorageWrapper.Data.EmailExportDirectory);

            Assert.Greater(0, emailAddresses.Count);
        }
    }
}
