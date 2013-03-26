using System;
using ContractStatisticsAnalyser;
using Data.Entities;

namespace UserInterface.ContractRelated
{
    public class TermsRetriever
    {
        public static TermsEntity GetFromUser()
        {
            var rate = InputReceiver.GetInt("Enter daily rate");
            var startDate = InputReceiver.GetDate("Enter contract start date");
            var duration = InputReceiver.GetInt("Enter contract duration in weeks");
            var lieu = InputReceiver.GetInt("Enter lieu payment period in weeks");
            var weeklyExpenses = InputReceiver.GetInt("Enter weekly expenses");
            Console.WriteLine("");

            return new TermsEntity(startDate, duration, rate, lieu, 0.04, 0.16, weeklyExpenses, DateTime.Now);
        }
    }
}