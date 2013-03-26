using System.Collections.Generic;
using System.IO;
using Data.Entities;
using Data.Invoice;
using Serialisation;

namespace Data.EntityWrappers.Clients
{
    public class DefaultClientsWrappers : IClientsWrapper
    {
        private List<ClientEntity> _defaultConfig = new List<ClientEntity>
            {
            new ClientEntity {
                Id = 0,
                PointOfContactEmail = "murados91@gmail.com",
                PointOfContactName = "Nathan Murados",
                CompanyInformationEntity = new CompanyInformationEntity
                {
                    Name = "Nathan Murados",
                    AddressLine1 = "134 Faggs Road",
                    Locality = "Feltham",
                    PostalTown = "Middlesex",
                    PostCode = "TW14 0LH"
                }
            },
            new ClientEntity {
                Id = 1,
                PointOfContactEmail = "Alison@wildeassociates.com",
                PointOfContactName = "Allison",
                CompanyInformationEntity = new CompanyInformationEntity
                {
                    Name = "Wilde Associates",
                    AddressLine1 = "Station Road",
                    Locality = "Newport Pagnell",
                    PostalTown = "Buckinghamshire",
                    PostCode = "MK16 0A"
                }
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
