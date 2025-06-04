using System.Threading.Tasks;

namespace CacheClient.Interfaces;

public interface IContent
{
    Task<string> ReadAsStringAsync();
}