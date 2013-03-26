using Data.Entities;

namespace Data.Invoice
{
    public class CompositeAddressCreator
    {
        public static string CreateAddress(CompanyInformationEntity companyInformationEntity)
        {
            return string.Format("{0}{1}{2}{3}{4}{5}",
                                 StringOrEmpty(companyInformationEntity.Name),
                                 StringOrEmpty(companyInformationEntity.AddressLine1),
                                 StringOrEmpty(companyInformationEntity.AddressLine2),
                                 StringOrEmpty(companyInformationEntity.Locality),
                                 StringOrEmpty(companyInformationEntity.PostalTown),
                                 StringOrEmpty(companyInformationEntity.PostCode));
        }

        private static string StringOrEmpty(string str)
        {
            const string newLineChar = "\n";
            if (!string.IsNullOrEmpty(str))
            {
                return str + newLineChar;
            }
            return "";
        }
    }
}