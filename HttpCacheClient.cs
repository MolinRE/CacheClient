using CacheClient.Interfaces;
using CacheClient.Models;
using System.Text;

namespace CacheClient;

public class HttpCacheClient
{
    private readonly string _dumpDirectory;
    private readonly HttpClient _client;
    private readonly Encoding _encoding;

    public HttpCacheClient(
        string dumpDirectory,
        HttpClient? httpClient = null,
        int cacheLifeMinutes = 0,
        Encoding? overrideEncoding = null)
    {
        _dumpDirectory = dumpDirectory;
        _client = httpClient ?? new();
        _encoding = overrideEncoding ?? Encoding.UTF8;
        
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
#if DEBUG
            Console.WriteLine($"Кэш удалён: {_dumpDirectory}");
#endif
        }
    }

    public async Task<IHttpResponseMessage> GetAsync(string requestUri, PageFormat format = PageFormat.HTML)
    {
        var url = new Uri(requestUri);
        var pageName = requestUri.GetMd5Hash() + "." + format.ToString().ToLower();
        var fileName = Path.Combine(_dumpDirectory, pageName);

        if (File.Exists(fileName))
        {
            return new CachedResponseMessage(fileName, _encoding);
        }

#if DEBUG
        Console.WriteLine("GET " + url.GetUrl());
#endif
        var response = await _client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return new RealHttpResponse(response, _encoding);
        }

        await using var stream = await response.Content.ReadAsStreamAsync();
        using var sr = new StreamReader(stream, _encoding);
        var result = await sr.ReadToEndAsync();
        if (!Directory.Exists(_dumpDirectory))
        {
            Directory.CreateDirectory(_dumpDirectory);
        }

        await File.WriteAllTextAsync(fileName, result, _encoding);
        return new RealHttpResponse(response, result);
    }
}
