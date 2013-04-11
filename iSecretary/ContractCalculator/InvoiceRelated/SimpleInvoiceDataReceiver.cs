using System.Collections.Generic;
using ContractStatisticsAnalyser;
using Data.Invoice;

namespace UserInterface.InvoiceRelated
{
    public class SimpleInvoiceDataReceiver
    {
        public static SimpleInvoiceDetails Get(List<ClientEntity> clients)
        {
            var client = ClientSelector.Get(clients);
            var invoiceNumber = UIRetriever.GetInt("Enter invoice number");
            var unitPrice = UIRetriever.GetDouble("Enter unit price");
            var quantity = UIRetriever.GetDouble("Enter unit quantity");
            var description = UIRetriever.GetString("Enter description");
            var notes = UIRetriever.GetString("Enter notes");

            return new SimpleInvoiceDetails
                {
                    Client = client,
                    Quantity = quantity,
                    Number = invoiceNumber,
                    UnitPrice = unitPrice,
                    CommentsOrSpecialInstructions = notes,
                    Description = description
                };
        }
    }
}