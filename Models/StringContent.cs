using CacheClient.Interfaces;

namespace CacheClient.Models;

public class StringContent(string content) : IContent
{
    public Task<string> ReadAsStringAsync()
    {
        return Task.FromResult(content);
    }
}