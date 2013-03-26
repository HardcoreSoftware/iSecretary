using System;
using Data.Entities;
using Extensions;

namespace ContractStatisticsAnalyser
{
    public class WeeklyAnalyses
    {
        public readonly int DaysWorked;
        public readonly int WeekIndex;
        private readonly TermsEntity _termsEntity;

        public WeeklyAnalyses(int weekIndex, int daysWorked, TermsEntity termsEntity)
        {
            WeekIndex = weekIndex;
            DaysWorked = daysWorked;
            _termsEntity = termsEntity;


            BusinessWeekStart = _termsEntity.Start.AddDays(7 * weekIndex).ToStartOfBusinessWeek();
            BusinessWeekEnd = _termsEntity.Start.AddDays(7 * weekIndex).ToEndOfBusinessWeek();
        }

        public double VatRate { get { return _termsEntity.VatRateMargin + _termsEntity.VatRateDue; } }
        public double BasicIncome { get { return _termsEntity.DailyRate * DaysWorked; } }
        public double TotalVat { get { return BasicIncome * VatRate; } }
        public double GrossIncome { get { return BasicIncome + TotalVat; } }
        public double VatMargin { get { return BasicIncome * _termsEntity.VatRateMargin; } }
        public double VatDue { get { return BasicIncome * _termsEntity.VatRateDue; } }
        public double NetIncome { get { return BasicIncome + (BasicIncome * _termsEntity.VatRateMargin); } }

        public DateTime BusinessWeekStart;
        public DateTime BusinessWeekEnd;
    }
}