using System.IO;
using Data.Entities;
using Serialisation;

namespace Data.EntityWrappers.Email
{
    public class TestEmailWrapper : IEmailWrapper
    {
        private EmailEntity _defaultData = new EmailEntity
            {
                Salutation = "Hi [FirstName],<br /><br />",
                Body = "Please find this weeks invoice attached.<br /><br />[NiceWeekend]<br /><br />",
                Signature = "Kind Regards</br />My Company<br /><br />"
            };
        public bool IsLoaded { get; private set; }
        public string Filename { get { return GetType().Name + ".xml"; } }
        public string Folder { get { return Repository.Folder; } }
        public string FullFileName { get { return Folder + Filename; } }
        public EmailEntity Data
        {
            get { return _defaultData; }
            private set { _defaultData = value; }
        }
        public void Load()
        {
            Data = SettingsReader.LoadEmailConfig(FullFileName);
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