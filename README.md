# Http Cache Client

### Usage

```csharp
var client = new HttpCacheClient("C:\temp\site.com_cache");

// First call sends actual request and stores response on disc
var pageContent = client.GetHtml("site.com/page.html");
// Second call first checks if page available on disc
var pageContentFromCache = client.GetHtml("site.com/page.html");

```