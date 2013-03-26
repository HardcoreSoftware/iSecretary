using System.IO;
using Data.Entities;
using Serialisation;

namespace Data.EntityWrappers.CompanyInformation
{
    public class DefaultCompanyInformationWrapper : ICompanyInformationWrapper
    {
        private CompanyInformationEntity _defaultData = new CompanyInformationEntity
        {
            Name = "Hardcore Software LTD",
            Slogan = "Software Design and Development",
            AddressLine1 = "134 Faggs Road",
            PostalTown = "Middlesex",
            PostCode = "TW14 0LH",
            Country = "United Kingdom",
            WebsiteUrl = "www.hardcoresoftware.co.uk",
            CellPhone = "+44 (0) 7946 865 068",
            OfficePhone = "+44 (0) 208 867 0410"
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