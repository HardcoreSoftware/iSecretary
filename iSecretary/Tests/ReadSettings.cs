using System.Globalization;
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
    class ReadSettings
    {
        [Test]
        public void ReadDefaultSmtp()
        {
            var x = new DefaultSmtpWrapper();
            Assert.IsFalse(x.IsLoaded);
            x.Load();
            Assert.IsTrue(x.IsLoaded);
            Assert.IsTrue(x.Exists());
            Assert.IsNotNullOrEmpty(x.Data.From);
            Assert.IsNotNullOrEmpty(x.Data.Host);
            Assert.IsNotNullOrEmpty(x.Data.CredentialsAccount);
            Assert.IsNotNullOrEmpty(x.Data.CredentialsPassword);
        }

        [Test]
        public void ReadDefaultEmail()
        {
            var x = new DefaultEmailWrapper();
            Assert.IsFalse(x.IsLoaded);
            x.Load();
            Assert.IsTrue(x.IsLoaded);
            Assert.IsTrue(x.Exists());
            Assert.IsNotNullOrEmpty(x.Data.Salutation);
            Assert.IsNotNullOrEmpty(x.Data.Body);
            Assert.IsNotNullOrEmpty(x.Data.Signature);
        }

        [Test]
        public void ReadDefaultInvoiceConfig()
        {
            var x = new DefaultInvoiceWrapper();
            Assert.IsFalse(x.IsLoaded);
            x.Load();
            Assert.IsTrue(x.IsLoaded);
            Assert.IsTrue(x.Exists());

            Assert.IsNotNullOrEmpty(x.Data.FooterText);

            Assert.IsNotNull(x.Data.AccountDetails);
            Assert.IsNotNullOrEmpty(x.Data.AccountDetails.Name);
            Assert.IsNotNullOrEmpty(x.Data.AccountDetails.Number);
            Assert.IsNotNullOrEmpty(x.Data.AccountDetails.SortCode);

            Assert.IsNotNull(x.Data.ChargeableJob);
            Assert.IsNotNullOrEmpty(x.Data.ChargeableJob.Quantity.ToString(CultureInfo.InvariantCulture));
            Assert.IsNotNullOrEmpty(x.Data.ChargeableJob.Description);
            Assert.IsNotNullOrEmpty(x.Data.ChargeableJob.UnitPrice.ToString(CultureInfo.InvariantCulture));
        }

        [Test]
        public void ReadDefaultCompanyInformationEntity()
        {
            var x = new DefaultCompanyInformationWrapper();
            Assert.IsFalse(x.IsLoaded);
            x.Load();
            Assert.IsTrue(x.IsLoaded);
            Assert.IsTrue(x.Exists());

            Assert.IsNotNull(x.Data);
            Assert.IsNotNullOrEmpty(x.Data.Name);
            Assert.IsNotNullOrEmpty(x.Data.Slogan);
            Assert.IsNotNullOrEmpty(x.Data.AddressLine1);
            Assert.IsNotNullOrEmpty(x.Data.PostalTown);
            Assert.IsNotNullOrEmpty(x.Data.WebsiteUrl);
            Assert.IsNotNullOrEmpty(x.Data.CellPhone);
            Assert.IsNotNullOrEmpty(x.Data.OfficePhone);
            Assert.IsNotNullOrEmpty(x.Data.PostCode);
            Assert.IsNotNullOrEmpty(x.Data.Country);
        }

        [Test]
        public void ReadDefaultClientConfig()
        {
            var x = new DefaultClientsWrappers();
            Assert.IsFalse(x.IsLoaded);
            x.Load();
            Assert.IsTrue(x.IsLoaded);
            Assert.IsTrue(x.Exists());
            Assert.IsNotNull(x.Data);

            foreach (var clientConfig in x.Data)
            {
                Assert.IsNotNullOrEmpty(clientConfig.PointOfContactEmail);
                Assert.IsNotNullOrEmpty(clientConfig.PointOfContactName);
                Assert.IsNotNull(clientConfig.CompanyInformationEntity);
                Assert.IsNotNullOrEmpty(clientConfig.CompanyInformationEntity.Name);
                Assert.IsNotNullOrEmpty(clientConfig.CompanyInformationEntity.AddressLine1);
                Assert.IsNotNullOrEmpty(clientConfig.CompanyInformationEntity.Locality);
                Assert.IsNotNullOrEmpty(clientConfig.CompanyInformationEntity.PostalTown);
                Assert.IsNotNullOrEmpty(clientConfig.CompanyInformationEntity.PostCode);   
            }
        }

        [Test]
        public void ReadDefaultTermsConfig()
        {
            var x = new DefaultTermsWrapper();
            Assert.IsFalse(x.IsLoaded);
            x.Load();
            Assert.IsTrue(x.IsLoaded);
            Assert.IsTrue(x.Exists());
            Assert.IsNotNull(x.Data);

            Assert.IsNotNull(x.Data);
            Assert.IsNotNull(x.Data.WeeklyExpenses);
            Assert.IsNotNull(x.Data.Start);
            Assert.IsNotNull(x.Data.DurationWeeks);
            Assert.IsNotNull(x.Data.DailyRate);
            Assert.IsNotNull(x.Data.LieuPaymentWeeks);
            Assert.IsNotNull(x.Data.VatRateMargin);
            Assert.IsNotNull(x.Data.VatRateDue);
        }

        [Test]
        public void ReadDefaultStorageConfig()
        {
            var x = new DefaultStorageWrapper();
            Assert.IsFalse(x.IsLoaded);
            x.Load();
            Assert.IsTrue(x.IsLoaded);
            Assert.IsTrue(x.Exists());
            Assert.IsNotNullOrEmpty(x.Data.InvoiceDirectory);
        }
    }
}
