using Data.Invoice;

namespace Data.Entities
{
    public class InvoiceEntity : IEntity
    {
        public string FooterText { get; set; }
        public string Notes { get; set; }
        public AccountDetails AccountDetails { get; set; }
        //public CompanyInformationEntity CompanyInformationEntity { get; set; }
        //public ClientConfig Client { get; set; }
        public int ClientId { get; set; }
        public ChargeableJob ChargeableJob { get; set; }
        public WeeklyInvoiceDetails WeeklyInvoiceDetails { get; set; }
    }
}