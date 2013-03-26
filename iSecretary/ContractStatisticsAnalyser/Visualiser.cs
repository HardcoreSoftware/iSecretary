using Data.Entities;

namespace ContractStatisticsAnalyser
{
    public class Visualiser
    {
        public static void Visualise(TermsEntity terms)
        {
            var results = Analyser.Analyse(terms);
            Printer.Print(results);
        }
    }
}