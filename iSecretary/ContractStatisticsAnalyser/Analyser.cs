using Data.Entities;

namespace ContractStatisticsAnalyser
{
    public class Analyser
    {
        public static Results Analyse(TermsEntity termsEntity)
        {
            var currentDate = termsEntity.Start;
            var daysWorkedThisWeek = 0;
            var contractAnalysesResults = new Results(termsEntity);

            var weekIndex = 0;
            for (var day = 0; day < 7 * (termsEntity.DurationWeeks + termsEntity.LieuPaymentWeeks); day++)
            {
                if (!DateInterogater.IsWeekend(currentDate) && day < 7 * termsEntity.DurationWeeks)
                {
                    daysWorkedThisWeek += 1;
                }
                else if (daysWorkedThisWeek > 0)
                {
                    var weeklyAnalyses = new WeeklyAnalyses(weekIndex, daysWorkedThisWeek, termsEntity);

                    contractAnalysesResults.WeeklyAnalyseses.Add(weeklyAnalyses);

                    daysWorkedThisWeek = 0;
                    weekIndex++;
                }
                currentDate = currentDate.AddDays(1);
            }
            return contractAnalysesResults;
        }
    }
}