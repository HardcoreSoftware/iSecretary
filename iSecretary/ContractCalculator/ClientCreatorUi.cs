using System;
using ContractStatisticsAnalyser;
using Data;
using Data.Entities;
using Data.Invoice;
using Extensions;

namespace UserInterface
{
    public class ClientCreatorUi
    {
        public static void AddClient(Repository repo)
        {
            var client = new ClientEntity
                {
                    PointOfContactName = UIRetriever.GetString(Nameof<ClientEntity>.Property(e => e.PointOfContactName)),
                    PointOfContactEmail = UIRetriever.GetString(Nameof<ClientEntity>.Property(e => e.PointOfContactEmail)),
                    CompanyInformationEntity = new CompanyInformationEntity
                        {
                            Name = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Name)),
                            Slogan = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Slogan), true),
                            AddressLine1 = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.AddressLine1)),
                            AddressLine2 = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.AddressLine2), true),
                            Locality = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Locality)),
                            PostalTown = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.PostalTown)),
                            PostCode = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.PostCode)),
                            Country = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Country), true),
                            WebsiteUrl = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.WebsiteUrl), true),
                            CellPhone = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.CellPhone), true),
                            OfficePhone = UIRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.OfficePhone), true)
                        }
                };

            repo.ClientsWrapper.Data.Add(client);
            repo.ClientsWrapper.Save();

            Console.WriteLine("\nNew client \"{0}\" added.\n", client.PointOfContactName);
        }
    }
}