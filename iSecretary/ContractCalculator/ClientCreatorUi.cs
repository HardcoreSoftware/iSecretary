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
                    PointOfContactName = InputReceiver.GetString(Nameof<ClientEntity>.Property(e => e.PointOfContactName)),
                    PointOfContactEmail = InputReceiver.GetString(Nameof<ClientEntity>.Property(e => e.PointOfContactEmail)),
                    CompanyInformationEntity = new CompanyInformationEntity
                        {
                            Name = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Name)),
                            Slogan = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Slogan), true),
                            AddressLine1 = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.AddressLine1)),
                            AddressLine2 = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.AddressLine2), true),
                            Locality = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Locality)),
                            PostalTown = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.PostalTown)),
                            PostCode = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.PostCode)),
                            Country = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.Country), true),
                            WebsiteUrl = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.WebsiteUrl), true),
                            CellPhone = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.CellPhone), true),
                            OfficePhone = InputReceiver.GetString(Nameof<CompanyInformationEntity>.Property(e => e.OfficePhone), true)
                        }
                };

            repo.ClientsWrapper.Data.Add(client);
            repo.ClientsWrapper.Save();

            Console.WriteLine("\nNew client \"{0}\" added.\n", client.PointOfContactName);
        }
    }
}