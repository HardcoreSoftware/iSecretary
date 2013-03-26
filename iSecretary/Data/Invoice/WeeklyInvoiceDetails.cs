namespace Data.Invoice
{
    public class WeeklyInvoiceDetails : IInvoiceDetails
    {
        //public string InvoiceFolder { get { return "C:\\Hardcore Software\\ISec\\Invoices"; } }
        public string CommentsOrSpecialInstructions { get; set; }
        public ClientEntity Client { get; set; }

        public double HourlyRate { get; set; }
        public int Number { get; set; }
        public double ChargeableHours { get; set; }
    }
}