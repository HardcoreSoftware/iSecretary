using System.IO;
using Data.Entities;
using Data.Invoice;
using Serialisation;

namespace Data.EntityWrappers.WeeklyInvoice
{
    public class TestInvoiceWrapper : IInvoiceWrapper
    {
        private InvoiceEntity _defaultData = new InvoiceEntity
        {
            ChargeableJob = new ChargeableJob
            {
                Description = "Testing",
                Quantity = 1,
                UnitPrice = 37.5
            },
            FooterText = "All prices shown are in GBP unless otherwise stated. " +
                         "My Company is a registered company. " +
                         "Payment should be made within 7 days by cheque or bank transfer. " +
                         "Cheques should be made out to \"my Company\". " +
                         "For payment by bank transfer, details as follows quoting the invoice number:",
            AccountDetails = new AccountDetails
            {
                Name = "My Company ",
                Number = "12345678",
                SortCode = "11-22-33"
            },
            Notes = "TEST - PLEASE IGNORE"
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