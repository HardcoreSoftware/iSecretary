using System.IO;
using Data.Entities;
using Serialisation;

namespace Data.EntityWrappers.CompanyInformation
{
    public class TestCompanyInformationWrapper : ICompanyInformationWrapper
    {
        private CompanyInformationEntity _defaultData = new CompanyInformationEntity
            {
                Name = "My Company",
                Slogan = "Slogan!",
                AddressLine1 = "Line 1",
                PostalTown = "PostalTown",
                PostCode = "AB12 3CD",
                Country = "Country",
                WebsiteUrl = "www.test.com",
                CellPhone = "+44 (0) 1234 567 890",
                OfficePhone = "+44 (0) 1234 567 890"
            };
        public bool IsLoaded { get; private set; }
        public string Filename { get { return GetType().Name + ".xml"; } }
        public string Folder { get { return Repository.Folder; } }
        public string FullFileName { get { return Folder + Filename; } }
        public CompanyInformationEntity Data
        {
            get { return _defaultData; }
            private set { _defaultData = value; }
        }
        public void Load()
        {
            Data = SettingsReader.LoadCompanyInformationConfig(FullFileName);
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