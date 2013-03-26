using System.IO;
using Data.Entities;
using Serialisation;

namespace Data.EntityWrappers.Email
{
    public class DefaultEmailWrapper : IEmailWrapper
    {
        private EmailEntity _defaultData = new EmailEntity
        {
            Salutation = "Hi [FirstName],<br /><br />",
            Body = "Please find this weeks invoice attached.<br /><br />[NiceWeekend]<br /><br />",
            Signature = "<div>-- <br><span>Regards,<br>Nathan Murados <small style=\"font-size:10px\">BSc (Hons) MSc</small><br>Director, Hardcore Software Ltd<br><br>LinkedIn: <a href=\"http://uk.linkedin.com/pub/nathan-murados/b/392/892/\" target=\"_blank\">Nathan  Murados</a><br>Email: <a href=\"mailto:nathan@hardcoresoftware.co.uk\" target=\"_blank\">nathan@hardcoresoftware.co.uk</a><br>Mobile: <a href=\"tel:00447946865068\" value=\"00447946865068\" target=\"_blank\">+44 (0)7946 865 068</a><br>Office: <a href=\"tel:00442088670410\" value=\"00442088670410\" target=\"_blank\">+44 (0)208 867 0410</a><br>Web: <a href=\"http://www.hardcoresoftware.co.uk/\" target=\"_blank\">http://www.hardcoresoftware.<wbr>co.uk</a><br><br><span style=\"font-size:7.5pt;font-family:'Arial','sans-serif'\">This message is intended only for the use of the individual(s) to whom it is addressed and may contain information which is privileged and confidential the disclosure of which is prohibited by law. If you are not the intended recipient, please notify the sender immediately by e-mail that you have received this e-mail by mistake and delete it from your system. E-mail transmission cannot be guaranteed to be secure or error-free. The sender therefore does not accept liability for any errors or omissions in the contents of this message which arise as a result of e-mail transmission. HardcoreSoftware Limited has taken steps to ensure that this email and any attachments are virus-free, but it remains your responsibility to confirm and ensure this. We thank you for your co-operation. <br> <br> HardcoreSoftware Limited is registered in England and Wales company number 8189791. The registered office of HardcoreSoftware Limited is at 134 Faggs Road, Feltham, Middlesex, TW14 0LH.</span><div class=\"yj6qo\"></div><div class=\"adL\"></div></span></div>",
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