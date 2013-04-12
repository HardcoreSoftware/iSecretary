using System;
using System.Collections.Generic;
using System.IO;

namespace UserInterface
{
    public class DoorEntryTimesManager
    {
        public static void Show()
        {
            foreach (var day in GetDoorTimes())
            {
                PrintDayStats(day);
            }

            Console.WriteLine("Total overtime: {0}", _overtime);
        }

        static TimeSpan _overtime = new TimeSpan(0, 0, 0, 0);
        readonly static TimeSpan Day = new TimeSpan(0, 7, 30, 0);

        private static void PrintDayStats(IReadOnlyList<DateTime> dates)
        {
            var totalMinutesIn = new TimeSpan(0, 0, 0, 0);
            var totalMinutesOut = new TimeSpan(0, 0, 0, 0);

            DateTime lastDate = dates[0];

            var isIn = false;
            for (int i = 1; i < dates.Count; i++)
            {
                var instance = dates[i];
                isIn = !isIn;
                if (isIn)
                {
                    totalMinutesIn = totalMinutesIn.Add(instance.Subtract(lastDate));
                }
                else
                {
                    totalMinutesOut = totalMinutesOut.Add(instance.Subtract(lastDate));
                }
                lastDate = instance;
            }
            Console.WriteLine("Total minutes in: {0}, out: {1}, extra: {2}", totalMinutesIn, totalMinutesOut, totalMinutesIn.Subtract(Day));
            _overtime = _overtime.Add(totalMinutesIn.Subtract(Day));
        }

        private static IEnumerable<List<DateTime>> GetDoorTimes()
        {
            string path = "";
            while (!File.Exists(path))
            {
                Console.WriteLine("Enter the path of the text file containing your door entry times: ");
                path = Console.ReadLine();
            }

            Console.WriteLine("");

            var lines = File.ReadAllLines(path);

            var dates = new List<List<DateTime>>();

            var index = 0;
            foreach (var line in lines)
            {
                if (index == dates.Count)
                {
                    dates.Add(new List<DateTime>());
                }
                if (line.Length > 19)
                {
                    dates[index].Add(DateTime.Parse(line.Substring(0, 19)));
                }
                else if (line.Length == 0)
                {
                    index++;
                }
            }
            return dates;
        }
    }
}