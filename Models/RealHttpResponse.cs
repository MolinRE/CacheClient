using CacheClient.Interfaces;
using System.Net;
using System.Text;

namespace CacheClient.Models;

public class RealHttpResponse : IHttpResponseMessage
{
    public bool IsSuccessStatusCode { get; }

    public HttpStatusCode StatusCode { get; }

    public string? ReasonPhrase { get; }

    public IHttpContent Content { get; set; }

    private RealHttpResponse(HttpResponseMessage response)
    {
        IsSuccessStatusCode = response.IsSuccessStatusCode;
        StatusCode = response.StatusCode;
        ReasonPhrase = response.ReasonPhrase;
    }

    public RealHttpResponse(HttpResponseMessage response, Encoding encoding) : this(response)
    {
        Content = new WebContent(response.Content, encoding);
    }
    
    public RealHttpResponse(HttpResponseMessage response, string content) : this(response)
    {
        Content = new StringContent(content);
    }
}