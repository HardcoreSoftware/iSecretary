using System.Collections.Generic;
using Data.Invoice;

namespace Data.EntityWrappers.Clients
{
    public interface IClientsWrapper : IISecWrapper
    {
        List<ClientEntity> Data { get; }
    }
}