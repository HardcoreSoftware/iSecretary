using Data.Entities;

namespace Data.EntityWrappers.Storage
{
    public interface IStorageWrapper : IISecWrapper
    {
        StorageEntity Data { get; }
    }
}
