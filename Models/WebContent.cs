using CacheClient.Interfaces;

namespace CacheClient.Models;

public class WebContent : IHttpContent
{
    private readonly HttpContent _content;

    public WebContent(HttpContent content)
    {
        _content = content;
    }

    public Task<string> ReadAsStringAsync()
    {
        return _content.ReadAsStringAsync();
    }
}