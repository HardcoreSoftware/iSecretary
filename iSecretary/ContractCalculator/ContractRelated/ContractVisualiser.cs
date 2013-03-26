using Data;

namespace UserInterface.ContractRelated
{
    public class ContractVisualiser
    {
        public static void Visualise()
        {
            ContractStatisticsAnalyser.Visualiser.Visualise(TermsRetriever.GetFromUser());
        }

        public static void VisualiseExistingContract(Repository repo)
        {
            ContractStatisticsAnalyser.Visualiser.Visualise(repo.TermsWrapper.Data);
        }
    }
}