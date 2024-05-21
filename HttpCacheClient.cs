using System.Text;

namespace CacheClient;

public class HttpCacheClient
{
    private readonly string _dumpDirectory;
    private readonly HttpClient _client;

    public HttpCacheClient(string dumpDirectory)
    {
        _dumpDirectory = dumpDirectory;
        _client = new();
    }

    public HttpCacheClient(string dumpDirectory, HttpClient httpClient)
    {
        _dumpDirectory = dumpDirectory;
        _client = httpClient;
    }

    public async Task<string> GetHtml(string url, string name) => await GetHtml(new Uri(url), name);
    
    public async Task<string> GetHtml(Uri url, string name)
    {
        var segments = url.AbsolutePath.Split("/", StringSplitOptions.RemoveEmptyEntries);
        if (segments.All(p => p == string.Empty))
        {
            var sb = new StringBuilder();
            foreach (var ch in name)
            {
                sb.Append(Path.GetInvalidFileNameChars().Contains(ch) ? '_' : ch);
            }
            
            segments = new[] { sb.ToString() };
        }
        
        var fileName = Path.Combine(_dumpDirectory, Path.Combine(segments)) + ".html";
        var dir = Path.GetDirectoryName(fileName)!;

        if (File.Exists(fileName))
        {
            return await File.ReadAllTextAsync(fileName, Encoding.UTF8);
        }
    
        #if DEBUG
        Console.WriteLine("GET " + url.PathAndQuery);
        #endif
        
        var response = await _client.GetAsync(url);
        await using var stream = await response.Content.ReadAsStreamAsync();
        using var sr = new StreamReader(stream, Encoding.UTF8);
        var result = await sr.ReadToEndAsync();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        await File.WriteAllTextAsync(fileName, result, Encoding.UTF8);

        return result;
    }
}
