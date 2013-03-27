using System;
using ContractStatisticsAnalyser;
using Data.Entities;

namespace UserInterface.ContractRelated
{
    public class TermsRetriever
    {
        public static TermsEntity GetFromUser()
        {
            var rate = UserInputRetriever.GetInt("Enter daily rate");
            var startDate = UserInputRetriever.GetDate("Enter contract start date");
            var duration = UserInputRetriever.GetInt("Enter contract duration in weeks");
            var lieu = UserInputRetriever.GetInt("Enter lieu payment period in weeks");
            var weeklyExpenses = UserInputRetriever.GetInt("Enter weekly expenses");
            Console.WriteLine("");

            return new TermsEntity(startDate, duration, rate, lieu, 0.04, 0.16, weeklyExpenses, DateTime.Now);
        }
    }
}