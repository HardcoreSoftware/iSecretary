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
            var invoiceNumber = UserInputRetriever.GetInt("Enter invoice number");
            var unitPrice = UserInputRetriever.GetDouble("Enter unit price");
            var quantity = UserInputRetriever.GetDouble("Enter unit quantity");
            var description = UserInputRetriever.GetString("Enter description");
            var notes = UserInputRetriever.GetString("Enter notes");

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