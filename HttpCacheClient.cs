using CacheClient.Interfaces;
using CacheClient.Models;
using System.Text;

namespace CacheClient;

public class HttpCacheClient
{
    private readonly string _dumpDirectory;
    private readonly HttpClient _client;

    public HttpCacheClient(string dumpDirectory, HttpClient? httpClient = null, int cacheLifeMinutes = 0)
    {
        _dumpDirectory = dumpDirectory;
        _client = httpClient ?? new();
        
        if (cacheLifeMinutes > 0 && Directory.Exists(_dumpDirectory))
        {
            var di = new DirectoryInfo(_dumpDirectory);
            var files = di.GetFiles("*", SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                if ((DateTime.Now - files.Max(p => p.CreationTime)).TotalMinutes > cacheLifeMinutes)
                {
                    DeleteCache();
                }
            }
        }
    }

    public void DeleteCache()
    {
        if (Directory.Exists(_dumpDirectory))
        {
            Directory.Delete(_dumpDirectory, true);
        }
    }

    public async Task<IHttpResponseMessage> GetAsync(string requestUri, PageFormat format = PageFormat.HTML)
    {
        var url = new Uri(requestUri);

        string pageName = requestUri.GetMd5Hash() + "." + format.ToString().ToLower();
        
        var fileName = Path.Combine(_dumpDirectory, pageName);

        if (!File.Exists(fileName))
        {
#if DEBUG
        Console.WriteLine("GET " + url.PathAndQuery);
#endif
            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return new RealHttpResponse(response);
            }
            await using var stream = await response.Content.ReadAsStreamAsync();
            using var sr = new StreamReader(stream, Encoding.UTF8);
            var result = await sr.ReadToEndAsync();
            if (!Directory.Exists(_dumpDirectory))
            {
                Directory.CreateDirectory(_dumpDirectory);
            }
            await File.WriteAllTextAsync(fileName, result, Encoding.UTF8);
        }

        return new CachedResponseMessage(fileName);
    }
}
