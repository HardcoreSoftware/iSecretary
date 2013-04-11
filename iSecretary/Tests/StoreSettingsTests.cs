using System.IO;
using Data.EntityWrappers;
using Data.EntityWrappers.Clients;
using Data.EntityWrappers.CompanyInformation;
using Data.EntityWrappers.Email;
using Data.EntityWrappers.Smtp;
using Data.EntityWrappers.Storage;
using Data.EntityWrappers.Terms;
using Data.EntityWrappers.WeeklyInvoice;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class StoreSettingsTests
    {
        [Test]
        public void WriteTestInvoiceConfig()
        {
            var x = new TestInvoiceWrapper();
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }
        [Test]
        public void WriteTestSmtpConfig()
        {
            var x = new TestSmtpWrapper();
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }
        [Test]
        public void WriteTestEmailConfig()
        {
            var x = new TestEmailWrapper();
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }

        [Test]
        public void WriteDefaultInvoiceConfig()
        {  
            var x = new DefaultInvoiceWrapper();
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }
        [Test]
        public void WriteDefaultSmtpConfig()
        {
            var x = new DefaultSmtpWrapper(); 
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }
        
        [Test]
        public void WriteDefaultEmailConfig()
        {
            var x = new DefaultEmailWrapper();
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }
        
        [Test]
        public void WriteDefaultClientConfig()
        {
            var x = new DefaultClientsWrappers();
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }

        [Test]
        public void WriteDefaultCompanyInformationConfig()
        {
            var x = new DefaultCompanyInformationWrapper();
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }

        [Test]
        public void WriteDefaultTermsConfig()
        {
            var x = new DefaultTermsWrapper();
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }

        [Test]
        public void WriteDefaultStorageConfig()
        {
            var x = new DefaultStorageWrapper();
            x.Save();
            Assert.IsTrue(File.Exists(x.FullFileName));
        }

        //[Test]
        //public void WriteDefaultStorageConfigChangeSave()
        //{
        //    var x = new DefaultStorageWrapper();
        //    x.Save();
        //    Assert.IsTrue(File.Exists(x.FullFileName));

        //    x.Data.InvoiceDirectory = "sfg";

        //    x.Save();
        //}
    }
}
