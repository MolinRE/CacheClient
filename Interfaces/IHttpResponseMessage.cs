using System.Net;

namespace CacheClient.Interfaces;

public interface IHttpResponseMessage
{
    public bool IsSuccessStatusCode { get; }

    public HttpStatusCode StatusCode { get; }

    public string? ReasonPhrase { get; }
    
    public IHttpContent Content { get; set; }
}