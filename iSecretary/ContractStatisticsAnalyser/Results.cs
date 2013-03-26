using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Extensions;

namespace ContractStatisticsAnalyser
{
    public class Results
    {
        public readonly TermsEntity Terms;

        public DateTime Now { get; set; }

        public Results(TermsEntity terms)
        {
            Terms = terms;
            WeeklyAnalyseses = new List<WeeklyAnalyses>();
            Now = DateTime.Now;
        }

        public List<WeeklyAnalyses> WeeklyAnalyseses { get; set; }
        public double BasicIncomeEarned { get { return WeeklyAnalyseses.Sum(weeklyAnalysese => weeklyAnalysese.BasicIncome); } }
        public double TotalVat { get { return WeeklyAnalyseses.Sum(weeklyAnalysese => weeklyAnalysese.TotalVat); } }
        public double VatMargin { get { return WeeklyAnalyseses.Sum(weeklyAnalysese => weeklyAnalysese.VatMargin); } }
        public double TotalVatDue { get { return WeeklyAnalyseses.Sum(weeklyAnalysese => weeklyAnalysese.VatDue); } }
        public double GrossIncome { get { return WeeklyAnalyseses.Sum(weeklyAnalysese => weeklyAnalysese.GrossIncome); } }
        public double NetIncome { get { return WeeklyAnalyseses.Sum(weeklyAnalysese => weeklyAnalysese.NetIncome); } }


        public int? CurrentWeekIndexZeroBased
        {
            get
            {
                if (Terms.Start > Now)
                {
                    return null;
                }
                if (Terms.IsFullyPaid)
                {
                    return null;
                }
                var i = -1;
                foreach (var wa in WeeklyAnalyseses)
                {
                    if (Now.ToEndOfBusinessWeek() >= wa.BusinessWeekStart)
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
                return i;
            }
        }

        public int WorkDaysPaid
        {
            get
            {
                if (Terms.IsFullyPaid)
                {
                    return WeeklyAnalyseses.Sum(x => x.DaysWorked);
                }
                if (Terms.Start > Now)
                {
                    return 0;
                }
                var paidWeeks = CurrentWeekIndexZeroBased + 1 - Terms.LieuPaymentWeeks;
                return WeeklyAnalyseses.Take((int)(paidWeeks < 0 ? 0 : paidWeeks)).Sum(x => x.DaysWorked);
            }
        }
        public int WorkDaysCompletedExclusive
        {
            get
            {
                var daysInCompletedWeeks = WeeklyAnalyseses.Take((CurrentWeekIndexZeroBased ?? WeeklyAnalyseses.Count)).Sum(x => x.DaysWorked);
                if (Terms.IsFullyPaid)
                {
                    return daysInCompletedWeeks;
                }

                var weekStartDate = WeeklyAnalyseses[CurrentWeekIndexZeroBased ?? WeeklyAnalyseses.Count - 1].BusinessWeekEnd;
                while (weekStartDate.DayOfWeek != DayOfWeek.Monday)
                {
                    weekStartDate = weekStartDate.AddDays(-1);
                }
                var completionDate = Now;
                if (Now > Terms.WorkEnd)
                {
                    completionDate = Terms.WorkEnd;
                }

                return ((int)Math.Round(completionDate.Subtract(weekStartDate).TotalDays)) + daysInCompletedWeeks;
            }
        }
        public int WorkDaysRemainingInclusive
        {
            get
            {
                if (Terms.IsFullyPaid)
                {
                    return 0;
                }
                return Now.BusinessDaysUntil(Terms.WorkEnd);
            }
        }
        public int WorkDaysRemainingExclusive
        {
            get
            {
                if (Terms.IsFullyPaid)
                {
                    return 0;
                }
                return Now.AddDays(1).BusinessDaysUntil(Terms.WorkEnd);
            }
        }

        public double NetIncomeToDate
        {
            get
            {
                return (WorkDaysPaid * Terms.DailyRate) + (WorkDaysPaid * (Terms.DailyRate * Terms.VatRateMargin));
            }
        }
        public double GrossIncomeToDate
        {
            get
            {
                return (WorkDaysPaid * Terms.DailyRate) + (WorkDaysPaid * (Terms.DailyRate * Terms.VatRateTotal));
            }
        }

        public double VatDueToDate
        {
            get
            {
                return WorkDaysPaid * (Terms.DailyRate * Terms.VatRateDue);
            }
        }

        public double TotalCorporationTaxAprx
        {
            get { return ((Terms.DurationWeeks * 5 * Terms.DailyRate) - (Terms.WeeklyExpenses * Terms.DurationWeeks)) * 0.21; }
        }
    }
}