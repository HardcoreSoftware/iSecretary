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
            var invoiceNumber = InputReceiver.GetInt("Enter invoice number");
            var chargeableHours = InputReceiver.GetDouble("Enter chargeable hours");
            var hourlyRate = InputReceiver.GetDouble("Enter hourly rate");
            var notes = InputReceiver.GetString("Enter notes");

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