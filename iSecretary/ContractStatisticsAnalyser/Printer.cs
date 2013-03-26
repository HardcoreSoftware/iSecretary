using System;
using System.Linq;

namespace ContractStatisticsAnalyser
{
    public class Printer
    {
        public static void Print(Results car)
        {
            Formatting.PrintRow(false, "Week End", "Base", "VAT", "VAT-M", "VAT-D", "GROSS", "NET", "CUM.NET");

            for (var i = 0; i < car.WeeklyAnalyseses.Count; i++)
            {
                var wa = car.WeeklyAnalyseses[i];
                Formatting.PrintRow(
                    i == car.CurrentWeekIndexZeroBased,
                    Formatting.FormattedWeek(i, wa.BusinessWeekEnd),
                    wa.BasicIncome,
                    wa.TotalVat,
                    wa.VatMargin,
                    wa.VatDue,
                    wa.GrossIncome,
                    wa.NetIncome,
                    car.WeeklyAnalyseses.Take(i).Sum(x => x.NetIncome));
            }

            Formatting.PrintRow(false,
                                Formatting.FormattedWeek(
                                    car.WeeklyAnalyseses.Count - 1 + car.Terms.LieuPaymentWeeks,
                                    car.WeeklyAnalyseses.Last().BusinessWeekEnd.AddDays(car.Terms.LieuPaymentWeeks * 7)
                                ),
                                car.BasicIncomeEarned,
                                car.TotalVat,
                                car.VatMargin,
                                car.TotalVatDue,
                                car.GrossIncome,
                                car.NetIncome,
                                car.NetIncome);


            Console.WriteLine("");
            Console.WriteLine("Payment to date: {0} gross\t| {1} net", Formatting.FormatCurrencyWithPadding(false, car.GrossIncomeToDate), Formatting.FormatCurrencyWithPadding(false, car.NetIncomeToDate));
            Console.WriteLine("Remaining Value: {0} gross\t| {1} net", Formatting.FormatCurrencyWithPadding(false, car.GrossIncome - car.GrossIncomeToDate), Formatting.FormatCurrencyWithPadding(false, car.NetIncome - car.NetIncomeToDate));
            Console.WriteLine("VAT due to date: {0}", Formatting.FormatCurrencyWithPadding(false, car.VatDueToDate));
            Console.WriteLine("Aprx. Corp. tax: {0}", Formatting.FormatCurrencyWithPadding(false, car.TotalCorporationTaxAprx));
            Console.WriteLine("Value after tax: {0}", Formatting.FormatCurrencyWithPadding(false, car.NetIncome - car.TotalCorporationTaxAprx));

            Console.WriteLine("");


            PrintCurrentWeekAndWeeksRemaining(car);
            PrintStartedDaysAgoAndEndsInDays(car);
            PrintDaysWorkedDaysRemaining(car);

            Console.WriteLine("");
            Console.WriteLine("");
        }

        private static void PrintCurrentWeekAndWeeksRemaining(Results car)
        {
            if (car.Terms.Start > car.Now)
            {
                Console.WriteLine("This contract starts in {0} weeks.", Math.Round(car.Terms.Start.Subtract(car.Now).TotalDays / 7));
            }
            else if (!car.Terms.IsFullyPaid)
            {
                Console.WriteLine("You are currently in week {0} and have {1} working weeks remaining.", car.CurrentWeekIndexZeroBased + 1, car.Terms.DurationWeeks - car.CurrentWeekIndexZeroBased - car.Terms.LieuPaymentWeeks);
            }
        }

        private static void PrintDaysWorkedDaysRemaining(Results car)
        {
            if (car.Terms.IsFullyPaid)
            {
                Console.WriteLine("You worked {0} days.", car.WorkDaysCompletedExclusive);
            }
            else if (car.Terms.Start > car.Now)
            {
                Console.WriteLine("You have not started this contract yet.");                
            }
            else
            {
                Console.WriteLine("Excluding today, you have worked {0} days and have {1} days to go",
                                  car.WorkDaysCompletedExclusive,
                                  car.WorkDaysRemainingInclusive);
            }
        }

        private static void PrintStartedDaysAgoAndEndsInDays(Results car)
        {
            var ends = Math.Round(car.Terms.PaymentEnd.Subtract(car.Now).TotalDays);
            if (car.Terms.IsFullyPaid)
            {
                Console.WriteLine("This contract ended {0} days ago.", Math.Abs(ends));
            }
            else if (car.Terms.Start > car.Now)
            {
                Console.WriteLine("This contract starts in {0} days and ends in {1} days.", Math.Abs(Math.Round(car.Now.Subtract(car.Terms.Start).TotalDays)), ends);
            }
            else
            {
                Console.WriteLine("This contract started {0} days ago and ends in {1} days.", Math.Round(car.Now.Subtract(car.Terms.Start).TotalDays), ends);
            }
        }
    }
}