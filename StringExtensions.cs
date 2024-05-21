using System.Globalization;

namespace CacheClient;

public static class StringExtensions
{
    public static DateOnly ToDate(this string s, string format)
    {
        return DateOnly.ParseExact(s, format, CultureInfo.InvariantCulture);
    }

    public static string SubstringSoft(this string s, int startIndex)
    {
        if (startIndex == -1)
        {
            return s;
        }
        return s.Substring(startIndex);
    }
}