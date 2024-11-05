using CacheClient.Interfaces;
using System.Text;

namespace CacheClient.Models;

public class WebContent : IContent
{
    private readonly HttpContent _content;
    private readonly Encoding _encoding;

    public WebContent(HttpContent content, Encoding encoding)
    {
        _content = content;
        _encoding = encoding;
    }

    public Task<string> ReadAsStringAsync()
    {
        return _content.ReadAsStringAsync();
    }
}