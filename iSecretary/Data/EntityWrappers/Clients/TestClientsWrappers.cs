using System.Collections.Generic;
using System.IO;
using Data.Entities;
using Data.Invoice;
using Serialisation;

namespace Data.EntityWrappers.Clients
{
    public class TestClientsWrappers : IClientsWrapper
    {
        private List<ClientEntity> _defaultConfig = new List<ClientEntity>
        {
            new ClientEntity
            {
                CompanyInformationEntity = new CompanyInformationEntity
                {
                    Name = "Client Name",
                    AddressLine1 = "Address Line 1",
                    Locality = "Locality",
                    PostalTown = "Postaltown",
                    PostCode = "ZX98 7Y"
                },
                PointOfContactEmail = "client@test.com",
                PointOfContactName = "ClientName"
            }
        };

        public bool IsLoaded { get; private set; }
        public string Filename { get { return GetType().Name + ".xml"; } }
        public string Folder { get { return Repository.Folder; } }
        public string FullFileName { get { return Folder + Filename; } }
        public List<ClientEntity> Data
        {
            get { return _defaultConfig; }
            private set { _defaultConfig = value; }
        }
        public void Load()
        {
            Data = SettingsReader.LoadClientConfigs(FullFileName);
            IsLoaded = true;
        }
        public void Save()
        {
            Serialiser.ObjectToXml(Data, Folder, Filename);
        }
        public bool Exists()
        {
            return File.Exists(Folder + Filename);
        }
    }
}