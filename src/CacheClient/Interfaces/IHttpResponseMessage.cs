using System.IO;

namespace CacheClient.Interfaces;

public abstract class BaseResponse
{
    /// <summary>
    /// Name of the file than contains cached page
    /// </summary>
    public string FileName { get; }

    public IContent Content { get; protected set; } = null!;

    protected BaseResponse(string fileName)
    {
        FileName = Path.GetFileName(fileName);
    }
}