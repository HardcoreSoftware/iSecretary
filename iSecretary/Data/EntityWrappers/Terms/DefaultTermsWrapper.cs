using System;
using System.IO;
using Data.Entities;
using Serialisation;

namespace Data.EntityWrappers.Terms
{
    public class DefaultTermsWrapper : ITermsWrapper
    {
        private TermsEntity _defaultData = new TermsEntity
            {
                DailyRate = 330,
                DurationWeeks = 12,
                LieuPaymentWeeks = 1,
                Start = new DateTime(2013,01,25),
                VatRateDue = 0.16,
                VatRateMargin = 0.04,
                WeeklyExpenses = 250
            };
        public bool IsLoaded { get; private set; }
        public string Filename { get { return GetType().Name + ".xml"; } }
        public string Folder { get { return Repository.Folder; } }
        public string FullFileName { get { return Folder + Filename; } }
        public TermsEntity Data
        {
            get { return _defaultData; }
            private set { _defaultData = value; }
        }
        public void Load()
        {
            Data = SettingsReader.LoadTermsConfig(FullFileName);
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