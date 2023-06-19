using System.Collections.Generic;
using System.IO;

namespace DataEngine.Abstraction.Interfaces
{
    public interface IFileSystem
    {
        IEnumerable<string> GetFileNames(string directory);
        FileStream GetFileStream(string fileName, FileMode mode);
        FileStream Create(string fileName);
    }
}
