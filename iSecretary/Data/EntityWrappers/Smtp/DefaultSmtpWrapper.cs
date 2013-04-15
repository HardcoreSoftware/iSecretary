using System.IO;
using Data.Entities;
using Serialisation;

namespace Data.EntityWrappers.Smtp
{
    public class DefaultSmtpWrapper : ISmtpWrapper
    {
        private SmtpEntity _defaultData = new SmtpEntity
        {
            From = "info@hardcoresoftware.co.uk",
            Host = "auth.smtp.1and1.co.uk",
            CredentialsAccount = "info@hardcoresoftware.co.uk",
            CredentialsPassword = "yourPasswordHere"
        };

        public bool IsLoaded { get; private set; }
        public string Filename { get { return GetType().Name + ".xml"; } }
        public string Folder { get { return Repository.Folder; } }
        public string FullFileName { get { return Folder + Filename; } }
        public SmtpEntity Data
        {
            get { return _defaultData; }
            private set { _defaultData = value; }
        }

        public void Load()
        {
            Data = SettingsReader.LoadSmtpConfig(FullFileName);
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