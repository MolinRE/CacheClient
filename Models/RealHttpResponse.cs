using CacheClient.Interfaces;
using System.Net;
using System.Text;

namespace CacheClient.Models;

public class RealHttpResponse : BaseResponse
{
    public bool IsSuccessStatusCode { get; }

    public HttpStatusCode StatusCode { get; }

    public string? ReasonPhrase { get; }

    private RealHttpResponse(HttpResponseMessage response, string fileName)
        : base(fileName)
    {
        IsSuccessStatusCode = response.IsSuccessStatusCode;
        StatusCode = response.StatusCode;
        ReasonPhrase = response.ReasonPhrase;
    }

    public RealHttpResponse(HttpResponseMessage response, Encoding encoding, string fileName)
        : this(response, fileName)
    {
        Content = new WebContent(response.Content, encoding);
    }

    public RealHttpResponse(HttpResponseMessage response, string content, string fileName)
        : this(response, fileName)
    {
        Content = new StringContent(content);
    }
}