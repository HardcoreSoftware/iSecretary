using Data.Entities;

namespace Data.EntityWrappers.Email
{
    public interface IEmailWrapper : IISecWrapper
    {
        EmailEntity Data { get; }
    }
}