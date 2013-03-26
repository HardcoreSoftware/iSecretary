namespace Data.EntityWrappers
{
    public interface IISecWrapper
    {
        bool IsLoaded { get; }
        string Filename { get; }
        string Folder { get; }
        string FullFileName { get; }
        void Load();
        void Save();
        bool Exists();
    }
}