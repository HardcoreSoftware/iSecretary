using System.Collections.Generic;
using ContractStatisticsAnalyser;
using Data.Invoice;

namespace UserInterface.InvoiceRelated
{
    public class WeeklyInvoiceDataReceiver
    {
        public static WeeklyInvoiceDetails Get(List<ClientEntity> clients)
        {
            var client = ClientSelector.Get(clients);
            var invoiceNumber = UserInputRetriever.GetInt("Enter invoice number");
            var chargeableHours = UserInputRetriever.GetDouble("Enter chargeable hours");
            var hourlyRate = UserInputRetriever.GetDouble("Enter hourly rate");
            var notes = UserInputRetriever.GetString("Enter notes");

            return new WeeklyInvoiceDetails
                {
                    Client = client,
                    HourlyRate = hourlyRate,
                    Number = invoiceNumber,
                    ChargeableHours = chargeableHours,
                    CommentsOrSpecialInstructions = notes
                };
        }
    }
}