using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ContractStatisticsAnalyser;
using Data;
using DataMiner.MozillaThunderbird;
using EmailProvider;
using IOInteraction;

namespace UserInterface.DataminingRelated
{
    class DataMiningInterface
    {

        public static void ShowMiningOptions(Repository repo)
        {
            var option = UIRetriever.GetOption("Select a task", new List<string>
                    {
                        string.Format("Mine all addresses in '{0}'",repo.StorageWrapper.Data.MineableDataDirectory),
                        "Converge Mozilla / LinkedIn lists\n",
                        "Send Test Email",
                        "Send CV To All Email Addresses [not active]",
                    });

            switch (option)
            {
                case 0: EmailMinerUi.Run(repo, Extractor.UniqueEmailAddressesFilename, Extractor.FailedToParseFilename); break;
                case 1: FileMerger.MergeFiles(repo); break;
                case 2: ClientMailer.MailTest(repo); break;
                case 3: ClientMailer.MailAll(repo); break;
            }

        }
    }

    public class ClientMailer
    {
        public static void MailTest(Repository repo)
        {
            var clientEntity = ClientSelector.Get(repo.ClientsWrapper.Data);

            var sender = new Sender(repo.SmtpWrapper.Data);
            sender.Send(clientEntity.PointOfContactEmail, Subject, GetRecruitmentBody(), GetCVs());
            sender.Send("ronan.darcy@gmail.com", Subject, GetRecruitmentBody(), GetCVs());
        }

        public static void MailAll(Repository repo)
        {
            var emailAddresses = File.ReadAllLines(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.ConvergedEmailAddressesFilename).ToList();

            var sender = new Sender(repo.SmtpWrapper.Data);

            if (!File.Exists(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.EmailAddressesSentSucesfully))
            {
                File.Create(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.EmailAddressesSentSucesfully);
                
            }

            var alreadySent = File.ReadAllLines(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.EmailAddressesSentSucesfully).ToList();

            const string mik = "melbakidze@gmail.com";

            if (!alreadySent.Contains(mik))
            {
                using (var w = File.AppendText(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.EmailAddressesSentSucesfully))
                {
                    w.WriteLine();
                }
                alreadySent.Add(mik);
            }
            
            var percentIndicator = 0;
            var processed = 0;
            foreach (var emailAddress in emailAddresses)
            {
                var percent = (int)Math.Round((100.0 / emailAddresses.Count) * processed);
                if (percent != percentIndicator)
                {
                    percentIndicator = percent;
                    Console.WriteLine("{0}% complete", percentIndicator);
                }

                if (!alreadySent.Contains(emailAddress) && !string.IsNullOrEmpty(emailAddress))
                {
                    if (IsValidEmail(emailAddress))
                    {
                        sender.Send(emailAddress, Subject, GetRecruitmentBody(), GetCVs());
                        Console.WriteLine("Sent to {0}", emailAddress);
                        alreadySent.Add(emailAddress);
                        using (var w = File.AppendText(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.EmailAddressesSentSucesfully))
                        {
                            w.WriteLine(emailAddress);
                        }
                    }else
                    {
                        Console.WriteLine("invalid: {0}", emailAddress);                        
                    }
                }
                else
                {
                    Console.WriteLine("skipping {0}", emailAddress);
                }
                processed++;
            }

            if (UIRetriever.GetBool(string.Format("View {0}?", repo.StorageWrapper.Data.MineableDataResultsDirectory)))
            {
                DirectoryVisualiser.ShowFile(repo.StorageWrapper.Data.MineableDataResultsDirectory + Extractor.EmailAddressesSentSucesfully);
            }
        }

        private const string Subject = "Software contractors - available from the 19th of April onwards";

        private static List<string> GetCVs()
        {
            const string cvDir = "C:\\Hardcore Software\\ISec\\CVsToSend\\";
            return FileRetriever.GetFiles(cvDir, "*.pdf").ToList();
        }

        private static string GetRecruitmentBody()
        {
            var sb = new StringBuilder();

            sb.Append("Hi,<br />");
            sb.Append("<br />");
            sb.Append("Could you let me know if you are in need of any ASP.NET C#/VB developers?<br />");
            sb.Append("<br />");
            sb.Append("I have attached two CV's, so if either are of interest, please get in touch.<br />");
            sb.Append("<br />");
            sb.Append("Michael is available from the 19th of April and myself from the 14th of May.<br />");
            sb.Append("<br />");
            sb.Append("Kind regards,<br />");
            sb.Append("<br />");
            sb.Append("<span style='color:#000;'>");
            sb.Append("Nathan Murados <small style='font-size:10px;'>BSc (Hons) MSc</small><br />");
            sb.Append("Director, Hardcore Software Ltd");
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append("Web: <a href='http://www.hardcoresoftware.co.uk/' target='_blank'>www.hardcoresoftware.co.uk</a><br />");
            sb.Append("Facebook: <a href='http://www.facebook.com/HardcoreSoftware'>www.facebook.com/HardcoreSoftware</a> | LinkedIn: <a href='http://uk.linkedin.com/pub/nathan-murados/b/392/892/'>Nathan Murados</a> | <a href='http://www.linkedin.com/company/hardcoresoftware-ltd/'>Hardcore Software</a><br />");
            sb.Append("Email: <a href='mailto:nathan@hardcoresoftware.co.uk' target='_blank'>nathan@hardcoresoftware.co.uk</a><br />");
            sb.Append("Mobile: <a href='tel:00447946865068' value='00447946865068' target='_blank>+44 (0)7946 865 068</a><br />");
            sb.Append("Office: <a href='el:00442088670410' value='0442088670410' target'blank'>+44 (0)208 867 0410</a><br />");
            sb.Append("<br />");
            sb.Append("<span style=\"font-size: 7.5pt; font-family: 'Arial','sans-serif'\">This message is intended only for the use of the individual(s) to ");
            sb.Append("    whom it is addressed and may contain information which is privileged and confidential the disclosure of which is prohibited by ");
            sb.Append("    law. If you are not the intended recipient, please notify the sender immediately by e-mail that you have received ");
            sb.Append("    this e-mail by mistake and delete it from your system. Alternatively if you do not wish to be contacted please get in touch and we will update our records. ");
            sb.Append("    E-mail transmission cannot be guaranteed to be secure or error-free.");
            sb.Append("    The sender therefore does not accept liability for any errors or omissions in the contents of this message which arise as");
            sb.Append("    a result of e-mail transmission. HardcoreSoftware Limited has taken steps to ensure that this email and any attachments are");
            sb.Append("    virus-free, but it remains your responsibility to confirm and ensure this.  We thank you for your co-operation.");
            sb.Append("    <br /><br />HardcoreSoftware Limited is registered in England and Wales company number 8189791. ");
            sb.Append("    The registered office of HardcoreSoftware Limited is at 134 Faggs Road, Feltham, Middlesex, TW14 0LH.");
            sb.Append("</span>");
            sb.Append("");
            sb.Append("</span>");

            return sb.ToString();
        }

        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                    @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
    }
}
