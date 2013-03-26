namespace Data.Invoice
{
    public interface IInvoiceDetails
    {
        //string InvoiceFolder { get; }
        int Number { get; set; }
        string CommentsOrSpecialInstructions { get; set; }
        ClientEntity Client { get; set; }
    }
}