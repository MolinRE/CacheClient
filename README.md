# Http Cache Client

### Usage

```csharp
var client = new HttpCacheClient(@"C:\temp\site.com_cache", cacheLifeMinutes: 24 * 60);

// First call sends actual request and stores response on disc
var response = client.GetAsync("site.com/page.html");
// Second call first checks if page available on disc
var responseFromCache = client.GetAsync("site.com/page.html");
```