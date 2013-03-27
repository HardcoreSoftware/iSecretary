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
                    PointOfContactName = UserInputRetriever.GetString(Nameof<ClientEntity>.Property(e => e.PointOfContactName)),
                    PointOfContactEmail = UserInputRetriever.GetString(Nameof<ClientEntity>.Property(e => e.PointOfContactEmail)),
                    CompanyInformationEntity = new CompanyInformationEntity
                        {
                            Name = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Name)),
                            Slogan = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Slogan), true),
                            AddressLine1 = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.AddressLine1)),
                            AddressLine2 = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.AddressLine2), true),
                            Locality = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Locality)),
                            PostalTown = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.PostalTown)),
                            PostCode = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.PostCode)),
                            Country = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Country), true),
                            WebsiteUrl = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.WebsiteUrl), true),
                            CellPhone = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.CellPhone), true),
                            OfficePhone = UserInputRetriever.GetString(Nameof<CompanyInformationEntity>.Property(e => e.OfficePhone), true)
                        }
                };

            repo.ClientsWrapper.Data.Add(client);
            repo.ClientsWrapper.Save();

            Console.WriteLine("\nNew client \"{0}\" added.\n", client.PointOfContactName);
        }
    }
}