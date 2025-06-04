using CacheClient.Interfaces;
using System.Net;
using System.Text;

namespace CacheClient.Models;

public class CachedResponseMessage : BaseResponse
{
    public CachedResponseMessage(string fileName, Encoding encoding)
        : base(fileName)
    {
        Content = new FileContent(fileName, encoding);
    }
}