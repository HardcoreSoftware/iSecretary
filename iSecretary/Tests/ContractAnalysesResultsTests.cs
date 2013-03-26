using System;
using ContractStatisticsAnalyser;
using Data.Entities;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ContractAnalysesResultsTests
    {
        public DateTime Start { get { return new DateTime(2010, 02, 01); } }

        private Results SetupBasic_LastDay()
        {
            return SetupBasic(Start, Start.AddDays(28), 1);
        }
        private Results SetupBasic_Incomplete()
        {
            return SetupBasic(Start, Start.AddDays(3), 1);
        }
        private Results SetupFourWeek_Incomplete()
        {
            return SetupBasic(Start, Start.AddDays(12), 4);
        }
        private static Results SetupBasic(DateTime start, DateTime now, int durationWeeks)
        {
            if (start.DayOfWeek != DayOfWeek.Monday) { throw new NotSupportedException("Assumption is 5 days per week are worked"); }
            var terms = new TermsEntity(start, durationWeeks, 100, 1, 0.04, 0.16, 0, now);
            var car = new Results(terms) { Now = now };

            for (var i = 0; i < durationWeeks; i++)
            {
                car.WeeklyAnalyseses.Add(new WeeklyAnalyses(i, 5, terms));
            }
            return car;
        }


        [Test]
        public void OneWeekBasic_Completed_IsComplete()
        {
            var car = SetupBasic_LastDay();
            Assert.AreEqual(true, car.Terms.IsFullyPaid);
        }
        [Test]
        public void OneWeekBasic_Completed_GrossIncome()
        {
            var car = SetupBasic_LastDay();
            Assert.AreEqual(600, car.GrossIncomeToDate);
        }
        [Test]
        public void OneWeekBasic_Completed_WorkDaysRemaining()
        {
            var car = SetupBasic_LastDay();
            Assert.AreEqual(0, car.WorkDaysRemainingInclusive);
        }
        [Test]
        public void OneWeekBasic_Completed_WorkDaysPaid()
        {
            var car = SetupBasic_LastDay();
            Assert.AreEqual(5, car.WorkDaysPaid);
        }

        [Test]
        public void OneWeekBasic_Incomplete_WorkDaysRemaining()
        {
            var car = SetupBasic(Start, Start.AddDays(2), 1);
            Assert.AreEqual(3, car.WorkDaysRemainingInclusive);
            Assert.AreEqual(2, car.WorkDaysRemainingExclusive);
        }

        [Test]
        public void FourWeekBasic_Incomplete_TermEnd()
        {
            var car = SetupBasic(Start, Start.AddDays(12), 4);
            Assert.AreEqual(new DateTime(2010, 02, 26), car.Terms.WorkEnd);
        }

        [Test]
        public void FourWeekBasic_Incomplete_CurrentWeek()
        {
            var car = SetupBasic(Start, Start, 4);
            Assert.AreEqual(0, car.CurrentWeekIndexZeroBased);

            car = SetupBasic(Start, Start.AddDays(1), 4);
            Assert.AreEqual(0, car.CurrentWeekIndexZeroBased);

            car = SetupBasic(Start, Start.AddDays(5), 4);
            Assert.AreEqual(0, car.CurrentWeekIndexZeroBased);

            car = SetupBasic(Start, Start.AddDays(6), 4);
            Assert.AreEqual(0, car.CurrentWeekIndexZeroBased);

            car = SetupBasic(Start, Start.AddDays(7), 4);
            Assert.AreEqual(1, car.CurrentWeekIndexZeroBased);

            car = SetupBasic(Start, Start.AddDays(8), 4);
            Assert.AreEqual(1, car.CurrentWeekIndexZeroBased);

            car = SetupBasic(Start, Start.AddDays(11), 4);
            Assert.AreEqual(1, car.CurrentWeekIndexZeroBased);

            car = SetupBasic(Start, Start.AddDays(12), 4);
            Assert.AreEqual(1, car.CurrentWeekIndexZeroBased);

            car = SetupBasic(Start, Start.AddDays(13), 4);
            Assert.AreEqual(1, car.CurrentWeekIndexZeroBased);

            car = SetupBasic(Start, Start.AddDays(14), 4);
            Assert.AreEqual(2, car.CurrentWeekIndexZeroBased);
        }

        [Test]
        public void FourWeekBasic_StartingOnFriday()
        {
            var start = Start.AddDays(5);
            var now = Start.AddDays(5);
            const int durationWeeks = 12;
            var terms = new TermsEntity(start, durationWeeks, 100, 1, 0.04, 0.16, 0, now);
            var car = new Results(terms) { Now = now };

            for (var i = 0; i < durationWeeks; i++)
            {
                car.WeeklyAnalyseses.Add(new WeeklyAnalyses(i, i == 0 ? 1 : 5, terms));
            }

            Assert.AreEqual(0, car.CurrentWeekIndexZeroBased);
            Assert.AreEqual(new DateTime(2010, 02, 05), car.WeeklyAnalyseses[0].BusinessWeekEnd);
            Assert.AreEqual(new DateTime(2010, 02, 01), car.WeeklyAnalyseses[0].BusinessWeekStart);
            Assert.AreEqual(0, car.VatDueToDate);
        }


        [Test]
        public void FourWeekBasic_FithFridayOfSecondContract()
        {
            var start = new DateTime(2013, 01, 25);
            var now = new DateTime(2013, 02, 22);
            const int durationWeeks = 12;
            var terms = new TermsEntity(start, durationWeeks, 330, 1, 0.04, 0.16, 0, now);
            var car = new Results(terms) { Now = now };

            for (var i = 0; i < durationWeeks; i++)
            {
                car.WeeklyAnalyseses.Add(new WeeklyAnalyses(i, i == 0 ? 1 : 5, terms));
            }

            Assert.AreEqual(4, car.CurrentWeekIndexZeroBased);
            Assert.AreEqual(16, car.WorkDaysPaid);
            Assert.AreEqual(845, Math.Round(car.VatDueToDate));
        }

        [Test]
        public void FourWeekBasic_SecondFridayOfSecondContract()
        {
            var start = new DateTime(2013, 01, 25);
            var now = new DateTime(2013, 02, 1);
            const int durationWeeks = 12;
            var terms = new TermsEntity(start, durationWeeks, 330, 1, 0.04, 0.16, 0, now);
            var car = new Results(terms) { Now = now };

            for (var i = 0; i < durationWeeks; i++)
            {
                car.WeeklyAnalyseses.Add(new WeeklyAnalyses(i, i == 0 ? 1 : 5, terms));
            }

            Assert.AreEqual(1, car.CurrentWeekIndexZeroBased);
            Assert.AreEqual(1, car.WorkDaysPaid);
        }

        [Test]
        public void FourWeekBasic_Future()
        {
            var start = new DateTime(2013, 01, 01);
            var now = new DateTime(2010, 01, 01);
            const int durationWeeks = 12;
            var terms = new TermsEntity(start, durationWeeks, 330, 1, 0.04, 0.16, 0, now);
            var car = new Results(terms) { Now = now };

            for (var i = 0; i < durationWeeks; i++)
            {
                car.WeeklyAnalyseses.Add(new WeeklyAnalyses(i, i == 0 ? 1 : 5, terms));
            }

            Assert.IsNull(car.CurrentWeekIndexZeroBased);
            Assert.AreEqual(0, car.WorkDaysPaid);
        }
    }
}
