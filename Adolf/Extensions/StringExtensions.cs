using System.Linq;

namespace Adolf.Extensions
{
    public static class StringExtensions
    {
        public static int LineCount(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return 0;

            return source.Count(c => c == '\r') + 1;
        }
    }
}