namespace Data.Entities
{
    public class StorageEntity :IEntity
    {
        public string InvoiceDirectory { get; set; }
        public string EmailExportDirectory { get; set; }
        public string EmailDataMiningResultsDirectory { get; set; }
    }
}