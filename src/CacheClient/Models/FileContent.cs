using CacheClient.Interfaces;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CacheClient.Models;

public class FileContent : IContent
{
    private readonly string _fileName;
    private readonly Encoding _encoding;

    public FileContent(string fileName, Encoding encoding)
    {
        _fileName = fileName;
        _encoding = encoding;
    }
    
    public async Task<string> ReadAsStringAsync()
    {
        return await File.ReadAllTextAsync(_fileName, _encoding);
    }
}