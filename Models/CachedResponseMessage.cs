using CacheClient.Interfaces;
using System.Net;

namespace CacheClient.Models;

public class CachedResponseMessage : IHttpResponseMessage
{
    public bool IsSuccessStatusCode { get; } = true;

    public HttpStatusCode StatusCode { get; } = HttpStatusCode.OK;

    public string? ReasonPhrase { get; } = null;

    public IHttpContent Content { get; set; }

    public CachedResponseMessage(string fileName)
    {
        Content = new FileContent(fileName);
    }
}