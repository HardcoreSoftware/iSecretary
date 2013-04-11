using System.Collections.Generic;
using System.Linq;
using ContractStatisticsAnalyser;
using Data.Invoice;

namespace UserInterface
{
    public class ClientSelector
    {
        public static ClientEntity Get(List<ClientEntity> clients)
        {
            var options = clients.Select(x => x.CompanyInformationEntity.Name + " - (" + x.PointOfContactEmail + ")").ToList();

            var option = UIRetriever.GetOption("Please choose a client", options);

            return clients[option];
        }
    }
}