namespace CacheClient;

public static class UriExtensions
{
    public static string GetUrl(this Uri uri)
    {
        if (uri.PathAndQuery == "/")
        {
            return uri.ToString();
        }

        return uri.PathAndQuery;
    }
}