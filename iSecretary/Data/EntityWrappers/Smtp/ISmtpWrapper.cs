using Data.Entities;

namespace Data.EntityWrappers.Smtp
{
    public interface ISmtpWrapper : IISecWrapper
    {
        SmtpEntity Data { get; }
    }
}