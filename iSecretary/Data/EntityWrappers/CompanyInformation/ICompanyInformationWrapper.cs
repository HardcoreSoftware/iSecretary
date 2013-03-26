using Data.Entities;

namespace Data.EntityWrappers.CompanyInformation
{
    public interface ICompanyInformationWrapper : IISecWrapper
    {
        CompanyInformationEntity Data { get; }
    }
}