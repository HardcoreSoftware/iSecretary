using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var repo = new Repository();
            var list = Extractor.GetEmailAddresses(repo.StorageWrapper.Data.EmailExportDirectory, out bad, 50);

            Assert.IsTrue(!list.Any(x => x.Contains(";")));
        }
    }
}
