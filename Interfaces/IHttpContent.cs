namespace CacheClient.Interfaces;

public interface IHttpContent
{
    Task<string> ReadAsStringAsync();
}