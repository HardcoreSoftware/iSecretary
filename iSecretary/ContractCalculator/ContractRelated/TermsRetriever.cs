using System;
using ContractStatisticsAnalyser;
using Data.Entities;

namespace UserInterface.ContractRelated
{
    public class TermsRetriever
    {
        public static TermsEntity GetFromUser()
        {
            var rate = UIRetriever.GetInt("Enter daily rate");
            var startDate = UIRetriever.GetDate("Enter contract start date");
            var duration = UIRetriever.GetInt("Enter contract duration in weeks");
            var lieu = UIRetriever.GetInt("Enter lieu payment period in weeks");
            var weeklyExpenses = UIRetriever.GetInt("Enter weekly expenses");
            Console.WriteLine("");

            return new TermsEntity(startDate, duration, rate, lieu, 0.04, 0.16, weeklyExpenses, DateTime.Now);
        }
    }
}