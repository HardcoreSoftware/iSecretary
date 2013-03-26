using System;
using System.Linq;

namespace Extensions
{
    public static class BusinessDays
    {
        public static DateTime ToEndOfBusinessWeek(this DateTime endOfWeek)
        {
            while (endOfWeek.DayOfWeek == DayOfWeek.Saturday || endOfWeek.DayOfWeek == DayOfWeek.Sunday)
            {
                endOfWeek = endOfWeek.AddDays(-1);
            }
            while (endOfWeek.DayOfWeek != DayOfWeek.Friday)
            {
                endOfWeek = endOfWeek.AddDays(1);
            }
            return endOfWeek;
        }

        public static DateTime ToStartOfBusinessWeek(this DateTime endOfWeek)
        {
            while (endOfWeek.DayOfWeek != DayOfWeek.Monday)
            {
                endOfWeek = endOfWeek.AddDays(-1);
            }
            return endOfWeek;
        }

        public static int BusinessDaysUntil(this DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
            {
                throw new ArgumentException("Incorrect last day " + lastDay);
            }

            var span = lastDay - firstDay;
            var businessDays = span.Days + 1;
            var fullWeekCount = businessDays/7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount*7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                var firstDayOfWeek = firstDay.DayOfWeek == DayOfWeek.Sunday ? 7 : (int) firstDay.DayOfWeek;
                var lastDayOfWeek = lastDay.DayOfWeek == DayOfWeek.Sunday ? 7 : (int) lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                {
                    lastDayOfWeek += 7;
                }
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7) // Both Saturday and Sunday are in the remaining time interval
                    {
                        businessDays -= 2;
                    }
                    else if (lastDayOfWeek >= 6) // Only Saturday is in the remaining time interval
                    {
                        businessDays -= 1;
                    }
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7) // Only Sunday is in the remaining time interval
                {
                    businessDays -= 1;
                }
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (
                var bh in
                    bankHolidays.Select(bankHoliday => bankHoliday.Date).Where(bh => firstDay <= bh && bh <= lastDay))
            {
                --businessDays;
            }

            return businessDays;
        }
    }
}
