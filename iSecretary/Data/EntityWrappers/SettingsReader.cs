using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Data.Entities;
using Data.Invoice;
using Extensions;

namespace Data.EntityWrappers
{
    public class SettingsReader
    {
        public static EmailEntity LoadEmailConfig(string fullFileName)
        {
            var xDoc = XDocument.Load(fullFileName);
            var a = xDoc.Root.Elements(Nameof<EmailEntity>.Property(e => e.Salutation)).First().Value;
            var b = xDoc.Root.Elements(Nameof<EmailEntity>.Property(e => e.Body)).First().Value;
            var c = xDoc.Root.Elements(Nameof<EmailEntity>.Property(e => e.Signature)).First().Value;

            return new EmailEntity
                {
                    Salutation = a,
                    Body = b,
                    Signature = c,
                };
        }

        public static SmtpEntity LoadSmtpConfig(string fullFileName)
        {
            var xDoc = XDocument.Load(fullFileName);
            var a = xDoc.Root.Elements(Nameof<SmtpEntity>.Property(e => e.From)).First().Value;
            var b = xDoc.Root.Elements(Nameof<SmtpEntity>.Property(e => e.Host)).First().Value;
            var c = xDoc.Root.Elements(Nameof<SmtpEntity>.Property(e => e.CredentialsAccount)).First().Value;
            var d = xDoc.Root.Elements(Nameof<SmtpEntity>.Property(e => e.CredentialsPassword)).First().Value;

            return new SmtpEntity
            {
                From = a,
                Host = b,
                CredentialsAccount = c,
                CredentialsPassword = d
            };
        }

        public static InvoiceEntity LoadInvoiceConfig(string fullFileName)
        {
            var xDoc = XDocument.Load(fullFileName);
            var a1 = xDoc.Root.Elements(typeof(AccountDetails).Name).Elements(Nameof<AccountDetails>.Property(e => e.Name)).First().Value;
            var a2 = xDoc.Root.Elements(typeof(AccountDetails).Name).Elements(Nameof<AccountDetails>.Property(e => e.Number)).First().Value;
            var a3 = xDoc.Root.Elements(typeof(AccountDetails).Name).Elements(Nameof<AccountDetails>.Property(e => e.SortCode)).First().Value;


            var e1 = xDoc.Root.Elements(typeof(ChargeableJob).Name).Elements(Nameof<ChargeableJob>.Property(e => e.Description)).First().Value;
            var e2 = Convert.ToDouble(xDoc.Root.Elements(typeof(ChargeableJob).Name).Elements(Nameof<ChargeableJob>.Property(e => e.Quantity)).First().Value);
            var e3 = Convert.ToDouble(xDoc.Root.Elements(typeof(ChargeableJob).Name).Elements(Nameof<ChargeableJob>.Property(e => e.UnitPrice)).First().Value);

            var f = xDoc.Root.Elements(Nameof<InvoiceEntity>.Property(e => e.FooterText)).First().Value;

            var g = xDoc.Root.Elements(Nameof<InvoiceEntity>.Property(e => e.Notes)).First().Value;

            WeeklyInvoiceDetails h = null;
            var wid = xDoc.Root.Elements(Nameof<InvoiceEntity>.Property(e => e.WeeklyInvoiceDetails)).FirstOrDefault();
            if (wid != null)
            {
                h = new WeeklyInvoiceDetails();
                h.HourlyRate = Convert.ToDouble(wid.Elements(Nameof<WeeklyInvoiceDetails>.Property(x => x.HourlyRate)).First().Value);
                h.Number = int.Parse(wid.Elements(Nameof<WeeklyInvoiceDetails>.Property(x => x.Number)).First().Value);
                h.ChargeableHours = Convert.ToDouble(wid.Elements(Nameof<WeeklyInvoiceDetails>.Property(x => x.ChargeableHours)).First().Value);
                h.CommentsOrSpecialInstructions = wid.Elements(Nameof<WeeklyInvoiceDetails>.Property(x => x.CommentsOrSpecialInstructions)).First().Value;
            }

            var i = Convert.ToInt32(xDoc.Root.Elements(Nameof<InvoiceEntity>.Property(e => e.ClientId)).First().Value);

            return new InvoiceEntity
            {
                AccountDetails = new AccountDetails
                {
                    Name = a1,
                    Number = a2,
                    SortCode = a3
                },
                ChargeableJob = new ChargeableJob
                {
                    Description = e1,
                    Quantity = e2,
                    UnitPrice = e3
                },
                FooterText = f,
                Notes = g,
                WeeklyInvoiceDetails = h,
                ClientId = i
            };
        }

        public static List<ClientEntity> LoadClientConfigs(string fullFileName)
        {
            var xDoc = XDocument.Load(fullFileName);

            var x = new List<ClientEntity>();

            foreach (var node in xDoc.Root.Elements())
            {
                var a0 = Convert.ToInt32(node.Elements(Nameof<ClientEntity>.Property(e => e.Id)).First().Value);
                var a1 = node.Elements(Nameof<ClientEntity>.Property(e => e.PointOfContactEmail)).First().Value;
                var a2 = node.Elements(Nameof<ClientEntity>.Property(e => e.PointOfContactName)).First().Value;
                var b1 = node.Elements(typeof(CompanyInformationEntity).Name).Elements(Nameof<CompanyInformationEntity>.Property(e => e.Name)).First().Value;
                var b2 = node.Elements(typeof(CompanyInformationEntity).Name).Elements(Nameof<CompanyInformationEntity>.Property(e => e.AddressLine1)).First().Value;
                var b3X = node.Elements(typeof(CompanyInformationEntity).Name).Elements(Nameof<CompanyInformationEntity>.Property(e => e.AddressLine2)).FirstOrDefault();
                var b3 = b3X == null ? "" : b3X.Value;
                var b4 = node.Elements(typeof(CompanyInformationEntity).Name).Elements(Nameof<CompanyInformationEntity>.Property(e => e.Locality)).First().Value;
                var b5 = node.Elements(typeof(CompanyInformationEntity).Name).Elements(Nameof<CompanyInformationEntity>.Property(e => e.PostalTown)).First().Value;
                var b6 = node.Elements(typeof(CompanyInformationEntity).Name).Elements(Nameof<CompanyInformationEntity>.Property(e => e.PostCode)).First().Value;

                x.Add(new ClientEntity
                {
                    Id = a0,
                    PointOfContactEmail = a1,
                    PointOfContactName = a2,
                    CompanyInformationEntity = new CompanyInformationEntity
                    {
                        Name = b1,
                        AddressLine1 = b2,
                        AddressLine2 = b3,
                        Locality = b4,
                        PostalTown = b5,
                        PostCode = b6,
                    }
                });
            }
            return x;
        }

        public static CompanyInformationEntity LoadCompanyInformationConfig(string fullFileName)
        {
            var xDoc = XDocument.Load(fullFileName);

            var b1 = xDoc.Root.Elements(Nameof<CompanyInformationEntity>.Property(e => e.Name)).First().Value;
            var b2 = xDoc.Root.Elements(Nameof<CompanyInformationEntity>.Property(e => e.Slogan)).First().Value;
            var b3 = xDoc.Root.Elements(Nameof<CompanyInformationEntity>.Property(e => e.AddressLine1)).First().Value;
            var b4 = xDoc.Root.Elements(Nameof<CompanyInformationEntity>.Property(e => e.PostalTown)).First().Value;
            var b5 = xDoc.Root.Elements(Nameof<CompanyInformationEntity>.Property(e => e.PostCode)).First().Value;
            var b6 = xDoc.Root.Elements(Nameof<CompanyInformationEntity>.Property(e => e.Country)).First().Value;
            var b7 = xDoc.Root.Elements(Nameof<CompanyInformationEntity>.Property(e => e.WebsiteUrl)).First().Value;
            var b8 = xDoc.Root.Elements(Nameof<CompanyInformationEntity>.Property(e => e.CellPhone)).First().Value;
            var b9 = xDoc.Root.Elements(Nameof<CompanyInformationEntity>.Property(e => e.OfficePhone)).First().Value;

            return new CompanyInformationEntity
                {
                    Name = b1,
                    Slogan = b2,
                    AddressLine1 = b3,
                    PostalTown = b4,
                    PostCode = b5,
                    Country = b6,
                    WebsiteUrl = b7,
                    CellPhone = b8,
                    OfficePhone = b9
                };
        }

        public static TermsEntity LoadTermsConfig(string fullFileName)
        {
            var xDoc = XDocument.Load(fullFileName);

            var b1 = Convert.ToDouble(xDoc.Root.Elements(Nameof<TermsEntity>.Property(e => e.WeeklyExpenses)).First().Value);
            var b2 = Convert.ToDateTime(xDoc.Root.Elements(Nameof<TermsEntity>.Property(e => e.Start)).First().Value);
            var b3 = Convert.ToInt32(xDoc.Root.Elements(Nameof<TermsEntity>.Property(e => e.DurationWeeks)).First().Value);
            var b4 = Convert.ToDouble(xDoc.Root.Elements(Nameof<TermsEntity>.Property(e => e.DailyRate)).First().Value);
            var b5 = Convert.ToInt32(xDoc.Root.Elements(Nameof<TermsEntity>.Property(e => e.LieuPaymentWeeks)).First().Value);
            var b6 = Convert.ToDouble(xDoc.Root.Elements(Nameof<TermsEntity>.Property(e => e.VatRateMargin)).First().Value);
            var b7 = Convert.ToDouble(xDoc.Root.Elements(Nameof<TermsEntity>.Property(e => e.VatRateDue)).First().Value);

            return new TermsEntity
            {
                WeeklyExpenses = b1,
                Start = b2,
                DurationWeeks = b3,
                DailyRate = b4,
                LieuPaymentWeeks = b5,
                VatRateMargin = b6,
                VatRateDue = b7
            };
        }

        public static StorageEntity LoadOperatingDirectoriesConfigs(string fullFileName)
        {
            var xDoc = XDocument.Load(fullFileName);

            var b1 = xDoc.Root.Elements(Nameof<StorageEntity>.Property(e => e.InvoiceDirectory)).First().Value;
            var b2 = xDoc.Root.Elements(Nameof<StorageEntity>.Property(e => e.MineableDataDirectory)).First().Value;
            var b3 = xDoc.Root.Elements(Nameof<StorageEntity>.Property(e => e.MineableDataResultsDirectory)).First().Value;
            
            return new StorageEntity
            {
                InvoiceDirectory = b1,
                MineableDataDirectory = b2,
                MineableDataResultsDirectory = b3
            };
        }
    }
}