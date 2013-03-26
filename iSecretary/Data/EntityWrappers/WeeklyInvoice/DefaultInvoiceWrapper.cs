using System.IO;
using Data.Entities;
using Data.Invoice;
using Serialisation;

namespace Data.EntityWrappers.WeeklyInvoice
{
    public class DefaultInvoiceWrapper : IInvoiceWrapper
    {
        private InvoiceEntity _defaultData = new InvoiceEntity
        {
            AccountDetails = new AccountDetails
            {
                Name = "HardcoreSoftware Limited",
                Number = "12345678",
                SortCode = "22-22-22"
            },
            ClientId = 1,
            ChargeableJob = new ChargeableJob
            {
                Description = "Services rendered hourly: On-site software development and consultancy",
                Quantity = 1,
                UnitPrice = 37.5

            },
            FooterText = "All prices shown are in GBP unless otherwise stated. " +
                         "HardcoreSoftware LTD is a UK registered company number 123456. " +
                         "VAT registration number: 123-4567-89. " +
                         "Registered address: HardcoreSoftware Ltd, 123 Roadsworth Road, Someplaceham, Somwhereshire, AB12 3CD. " +
                         "Payment should be made within 7 days by cheque or bank transfer. " +
                         "Cheques should be made out to \"Hardcoresoftware Limited\". " +
                         "For payment by bank transfer, details as follows quoting the invoice number:",
            Notes = "Graphnet Health Ltd",
            WeeklyInvoiceDetails = new WeeklyInvoiceDetails
                {
                    Number = 21,
                    ChargeableHours = 44,
                    CommentsOrSpecialInstructions = "Company Ltd",
                    HourlyRate = 37.5
                }
        };
        public bool IsLoaded { get; private set; }
        public string Filename { get { return GetType().Name + ".xml"; } }
        public string Folder { get { return Repository.Folder; } }
        public string FullFileName { get { return Folder + Filename; } }
        public InvoiceEntity Data
        {
            get { return _defaultData; }
            private set { _defaultData = value; }
        }
        public void Load()
        {
            Data = SettingsReader.LoadInvoiceConfig(FullFileName);
            IsLoaded = true;
        }
        public void Save()
        {
            Serialiser.ObjectToXml(Data, Folder, Filename);
        }
        public bool Exists()
        {
            return File.Exists(Folder + Filename);
        }
    }
}