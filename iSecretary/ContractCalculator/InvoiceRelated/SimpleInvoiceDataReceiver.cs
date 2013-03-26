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
            var invoiceNumber = InputReceiver.GetInt("Enter invoice number");
            var unitPrice = InputReceiver.GetDouble("Enter unit price");
            var quantity = InputReceiver.GetDouble("Enter unit quantity");
            var description = InputReceiver.GetString("Enter description");
            var notes = InputReceiver.GetString("Enter notes");

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