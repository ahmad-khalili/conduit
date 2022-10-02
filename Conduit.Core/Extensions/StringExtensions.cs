using System.Text.RegularExpressions;

namespace Conduit.Core.Extensions;

public static class StringExtensions
{
    public static string ToSlug(this string source)
    {
        var removedSpecial = Regex
            .Replace(source, "[^0-9A-Za-z ]+", "", RegexOptions.Compiled);

        var toSlug = removedSpecial.Replace(" ", "-").ToLower();

        return toSlug;
    }
}