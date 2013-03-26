using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;

namespace ContentProvider
{
    public class EmailBodyCreator
    {

        public static string Create(EmailEntity emailEntity, string pointOfContactName)
        {
            var sb = new StringBuilder();
            sb.AppendLine(emailEntity.Salutation.Replace("[FirstName]", pointOfContactName));
            sb.AppendLine(emailEntity.Body.Replace("[NiceWeekend]", RandomNiceWeekend()));
            sb.AppendLine(emailEntity.Signature);
            return sb.ToString();
        }

        private static string RandomNiceWeekend()
        {
            var phrases = new List<string>
                {
                    "Have a nice weekend. J",
                    "Enjoy the weekend.",
                    "Have a good weekend.",
                    "Have a great weekend. J",
                    "Enjoy your weekend. J"
                };

            var rand = new Random();
            return phrases[rand.Next(phrases.Count)];
        }
    }
}
