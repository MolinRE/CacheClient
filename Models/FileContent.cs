using CacheClient.Interfaces;
using System.Text;

namespace CacheClient.Models;

public class FileContent : IHttpContent
{
    private readonly string _fileName;

    public FileContent(string fileName)
    {
        _fileName = fileName;
    }
    
    public async Task<string> ReadAsStringAsync()
    {
        return await File.ReadAllTextAsync(_fileName, Encoding.UTF8);
    }
}