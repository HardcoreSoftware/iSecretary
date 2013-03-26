using System;
using Extensions;

namespace Data.Entities
{
    public class TermsEntity : IEntity
    {
        public double WeeklyExpenses { get; set; }
        public int DurationWeeks { get; set; }
        public int LieuPaymentWeeks { get; set; }
        public double DailyRate { get; set; }
        public double VatRateMargin { get; set; }
        public double VatRateDue { get; set; }
        public DateTime Start { get; set; }
        public readonly DateTime Now;

        public TermsEntity(DateTime start, int durationWeeks, double dailyRate, int paymentDelayWeeks, double vatRateMargin, double vatRateDue, double weeklyExpenses,DateTime now)
        {
            WeeklyExpenses = weeklyExpenses;
            Start = start;
            DurationWeeks = durationWeeks;
            DailyRate = dailyRate;
            LieuPaymentWeeks = paymentDelayWeeks;
            VatRateMargin = vatRateMargin;
            VatRateDue = vatRateDue;
            Now = now;
        }

        public TermsEntity()
        {
            Now = DateTime.Now;
        }

        public double VatRateTotal { get { return VatRateMargin + VatRateDue; } }

        public bool IsFullyPaid { get { return Now > PaymentEnd; } }
        public bool IsServed { get { return Now > WorkEnd; } }

        public DateTime PaymentEnd { get { return Start.AddDays((DurationWeeks + LieuPaymentWeeks) * 7); } }
        public DateTime WorkEnd
        {
            get
            {
                var roughEnd = Start.AddDays((DurationWeeks * 7) - 1);
                return roughEnd.ToEndOfBusinessWeek();
            }
        }
    }
}