using System;
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
    
    public static string GetMd5Hash(this string input)
    {
        // Use input string to calculate MD5 hash
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}