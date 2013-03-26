namespace Data.Invoice
{
    public class SimpleInvoiceDetails : IInvoiceDetails
    {
        //public string InvoiceFolder { get { return "C:\\Hardcore Software\\ISec\\Invoices"; } }
        public int Number { get; set; }
        public string CommentsOrSpecialInstructions { get; set; }
        public ClientEntity Client { get; set; }

        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string Description { get; set; }
    }
}