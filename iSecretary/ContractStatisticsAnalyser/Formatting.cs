using System;
using System.Globalization;

namespace ContractStatisticsAnalyser
{
    public class Formatting
    {
        private const int PaddingAmount = 9;

        public static void PrintRow(bool isCurrent, params object[] args)
        {
            var str = "";
            const string spacer = "";

            var finalArgs = new object[args.Length + 1];
            finalArgs[0] = spacer;

            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] is int)
                {
                    finalArgs[i + 1] = FormatCurrencyWithPadding(isCurrent, (int)args[i]);
                }
                else if (args[i] is double)
                {
                    finalArgs[i + 1] = FormatCurrencyWithPadding(isCurrent, (double)args[i]);
                }
                else
                {
                    finalArgs[i + 1] = (GetIsCurrentWeekDelimiter(isCurrent) + args[i]).PadLeft(PaddingAmount, ' ');
                }
                str += "{" + (i + 1) + "}{0}";
            }

            str = str.Substring(0, str.Length - 3);
            Console.WriteLine(str, finalArgs);
        }
        public static string GetIsCurrentWeekDelimiter(bool isCurrent)
        {
            return isCurrent ? ">" : " ";
        }

        public static string FormatCurrencyWithPadding(bool isCurrent, double number)
        {
            return (GetIsCurrentWeekDelimiter(isCurrent) + String.Format("{0:#,##0}", number)).PadLeft(PaddingAmount, ' ');
        }
        public static string FormatCurrency(double number)
        {
            return String.Format("{0:#,##0}", number);
        }

        public static string FormattedWeek(int totalWeeksCounted, DateTime currentDate)
        {
            return string.Format("{0} {1}", 
                                          (totalWeeksCounted + 1).ToString(CultureInfo.InvariantCulture).PadLeft(3, ' '), 
                                          currentDate.ToString("ddMM"));
        }
    }
}