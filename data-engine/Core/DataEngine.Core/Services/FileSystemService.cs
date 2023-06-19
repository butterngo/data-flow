using DataEngine.Abstraction.Interfaces;

namespace DataEngine.Core.Services
{
    public class FileSystemService : IFileSystem
    {
        public IEnumerable<string> GetFileNames(string directory)
            => Directory.EnumerateFiles(directory);

        public FileStream GetFileStream(string fileName, FileMode mode)
            => new FileStream(fileName, mode);

        public FileStream Create(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            return GetFileStream(fileName, FileMode.OpenOrCreate);
        }
    }
}
