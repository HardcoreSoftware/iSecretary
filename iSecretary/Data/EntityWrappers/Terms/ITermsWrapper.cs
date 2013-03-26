using Data.Entities;

namespace Data.EntityWrappers.Terms
{
    public interface ITermsWrapper : IISecWrapper
    {
        TermsEntity Data { get; }
    }
}