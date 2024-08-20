using CacheClient.Interfaces;

namespace CacheClient.Models;

public class StringContent(string content) : IHttpContent
{
    public Task<string> ReadAsStringAsync()
    {
        return Task.FromResult(content);
    }
}