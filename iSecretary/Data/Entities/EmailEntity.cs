namespace Data.Entities
{
    public class EmailEntity : IEntity
    {
        public string Salutation { get; set; }
        public string Body { get; set; }
        public string Signature { get; set; }
    }
}