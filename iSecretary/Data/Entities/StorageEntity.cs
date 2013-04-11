namespace Data.Entities
{
    public class StorageEntity :IEntity
    {
        public string InvoiceDirectory { get; set; }
        public string MineableDataDirectory { get; set; }
        public string MineableDataResultsDirectory { get; set; }
    }
}