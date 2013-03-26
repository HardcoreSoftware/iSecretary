using System;

namespace ContractStatisticsAnalyser
{
    public class DateInterogater
    {
        public static bool IsWeekend(DateTime currentDate)
        {
            return currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}