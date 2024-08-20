using CacheClient.Interfaces;
using System.Net;
using System.Text;

namespace CacheClient.Models;

public class CachedResponseMessage : IHttpResponseMessage
{
    public bool IsSuccessStatusCode { get; } = true;

    public HttpStatusCode StatusCode { get; } = HttpStatusCode.OK;

    public string? ReasonPhrase { get; } = null;

    public IHttpContent Content { get; set; }

    public CachedResponseMessage(string fileName, Encoding encoding)
    {
        Content = new FileContent(fileName, encoding);
    }
}