using CacheClient.Interfaces;
using System.Net;

namespace CacheClient.Models;

public class RealHttpResponse : IHttpResponseMessage
{
    public bool IsSuccessStatusCode { get; }

    public HttpStatusCode StatusCode { get; }

    public string? ReasonPhrase { get; }

    public IHttpContent Content { get; set; }

    public RealHttpResponse(HttpResponseMessage response)
    {
        IsSuccessStatusCode = response.IsSuccessStatusCode;
        StatusCode = response.StatusCode;
        ReasonPhrase = response.ReasonPhrase;
        Content = new WebContent(response.Content);
    }
}