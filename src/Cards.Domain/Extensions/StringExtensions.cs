using System.Text.RegularExpressions;

namespace Cards.Domain.Extensions
{
    public static class StringExtensions
    {
        public static MatchCollection Matches(this string input, string pattern)
        {
            return Regex.Matches(input, pattern);
        }
    }
}
