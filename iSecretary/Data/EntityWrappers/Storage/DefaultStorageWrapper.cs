using System.IO;
using Data.Entities;
using Serialisation;

namespace Data.EntityWrappers.Storage
{
    public class DefaultStorageWrapper : IStorageWrapper
    {
        private StorageEntity _defaultConfig = new StorageEntity
            {
                InvoiceDirectory = "C:\\Hardcore Software\\ISec\\Invoices\\",
                EmailExportDirectory = "C:\\Hardcore Software\\ISec\\MineableData\\",
                EmailDataMiningResultsDirectory = "C:\\Hardcore Software\\ISec\\MineableDataResults\\"
            };
    

        public bool IsLoaded { get; private set; }
        public string Filename { get { return GetType().Name + ".xml"; } }
        public string Folder { get { return Repository.Folder; } }
        public string FullFileName { get { return Folder + Filename; } }
        public StorageEntity Data
        {
            get { return _defaultConfig; }
            private set { _defaultConfig = value; }
        }
        public void Load()
        {
            Data = SettingsReader.LoadOperatingDirectoriesConfigs(FullFileName);
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