using Data.Entities;

namespace Data.EntityWrappers.WeeklyInvoice
{
    public interface IInvoiceWrapper : IISecWrapper
    {
        InvoiceEntity Data { get; }
    }
}