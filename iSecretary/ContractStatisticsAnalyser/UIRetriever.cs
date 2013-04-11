using System;
using System.Collections.Generic;

namespace ContractStatisticsAnalyser
{
    public class UIRetriever
    {
        public static int GetRestrictedInt(string question, List<int> allowed)
        {
            while (true)
            {
                Console.Write(question);
                var x = Console.ReadLine();
                EasterEggCheck(x);
                int i;
                if (Int32.TryParse(x, out i))
                {
                    if (allowed.Contains(i))
                    {
                        return i;
                    }
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        public static int GetInt(string question)
        {
            while (true)
            {
                Console.Write(question.Trim() + ": ");
                var x = Console.ReadLine();
                EasterEggCheck(x);
                int i;
                if (Int32.TryParse(x, out i))
                {
                    return i;
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        public static double GetDouble(string question)
        {
            while (true)
            {
                Console.Write(question.Trim() + ": ");
                var x = Console.ReadLine();
                EasterEggCheck(x);
                double i;
                if (double.TryParse(x, out i))
                {
                    return i;
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        public static DateTime GetDate(string question)
        {
            while (true)
            {
                Console.Write(question.Trim() + ": ");
                var x = Console.ReadLine();
                EasterEggCheck(x);
                DateTime i;
                if (DateTime.TryParse(x, out i))
                {
                    return i;
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        public static bool GetBool(string question)
        {
            while (true)
            {
                Console.Write("{0} [y/n]:", question.Trim() + " ");

                var x = Console.ReadLine();
                EasterEggCheck(x);
                bool i;
                if (Boolean.TryParse(x, out i))
                {
                    return i;
                }
                if (x != null)
                {
                    switch (x.ToUpper())
                    {
                        case "Y":
                        case "YES": return true;
                        case "N":
                        case "NO": return false;
                    }
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        private static void EasterEggCheck(string s)
        {
            if (s.ToUpper().Contains("FUCK"))
            {
                Console.WriteLine("Or how about you " + s.Trim() + " :)");
            }
            if (s.ToLower().Contains("dick") ||
                s.ToLower().Contains("bitch") ||
                s.ToLower().Contains("cunt") ||
                s.ToLower().Contains("twat") ||
                s.ToLower().Contains("cock") ||
                s.ToLower().Contains("dik") ||
                s.ToLower().Contains("dic") ||
                s.ToLower().Contains("knob") ||
                s.ToLower().Contains("anal") ||
                s.ToLower().Contains("ass") ||
                s.ToLower().Contains("arse"))
            {
                Console.WriteLine("You sir, are an " + s.Trim() + ".");
            }
        }

        public static string GetString(string question, bool allowNull = false)
        {
            Console.Write(question.Trim() + (allowNull ? " (optional): " : ": "));
            var str = Console.ReadLine();
            return str == string.Empty && allowNull ? null : str;
        }

        public static int GetOption(string question, List<string> options)
        {
            Console.WriteLine(question.Trim() + ": ");
            var allowed = new List<int>();
            for (var i = 0; i < options.Count; i++)
            {
                allowed.Add(i + 1);
                Console.WriteLine("  [{0}] {1}", i + 1, options[i]);
            }

            var incrementedOption = GetRestrictedInt("", allowed);
            incrementedOption--;

            Console.WriteLine("\"{0}\" selected.\n", options[incrementedOption].Replace("\n",""));
            return incrementedOption;
        }

        public static DateTime? GetNullableDate(string question)
        {
            while (true)
            {
                Console.Write(question.Trim() + ": ");
                var x = Console.ReadLine();
                if (x == string.Empty)
                {
                    return null;
                }
                EasterEggCheck(x);
                DateTime i;
                if (DateTime.TryParse(x, out i))
                {
                    return i;
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}