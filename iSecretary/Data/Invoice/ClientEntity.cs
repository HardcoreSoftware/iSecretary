using Data.Entities;

namespace Data.Invoice
{
    public class ClientEntity : IEntity
    {
        public int Id { get; set; }
        public CompanyInformationEntity CompanyInformationEntity { get; set; }
        public string PointOfContactName { get; set; }
        public string PointOfContactEmail { get; set; }
    }
}