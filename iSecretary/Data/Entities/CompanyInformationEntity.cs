namespace Data.Entities
{
    public class CompanyInformationEntity : IEntity
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Locality { get; set; }
        public string PostalTown { get; set; }
        public string PostCode { get; set; }
        public string Slogan { get; set; }
        public string Country { get; set; }
        public string WebsiteUrl { get; set; }
        public string CellPhone { get; set; }
        public string OfficePhone { get; set; }
    }
}