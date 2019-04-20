using System;
using System.Collections.Generic;
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
        
        public static string WordWrap(this string text, int maxLineLength )
        {
            var list = new List<string>();

            int currentIndex;
            var lastWrap = 0;
            var whitespace = new[] { ' ', '\r', '\n', '\t' };
            do
            {
                currentIndex = lastWrap + maxLineLength > text.Length ? text.Length : (text.LastIndexOfAny( new[] { ' ', ',', '.', '?', '!', ':', ';', '-', '\n', '\r', '\t' }, Math.Min( text.Length - 1, lastWrap + maxLineLength)  ) + 1);
                if( currentIndex <= lastWrap )
                    currentIndex = Math.Min( lastWrap + maxLineLength, text.Length );
                list.Add( text.Substring( lastWrap, currentIndex - lastWrap ).Trim( whitespace ) );
                lastWrap = currentIndex;
            } while( currentIndex < text.Length );

            return string.Join(Environment.NewLine, list);
        }
    }
}