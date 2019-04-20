using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Adolf
{
    public abstract class Html
    {
        public static string Clean(string inputString)
        {
            inputString = Regex.Replace(inputString, @"<div .*?>", Environment.NewLine);
            inputString = Regex.Replace(inputString, @"<li .*?>", Environment.NewLine + "  •   ");

            inputString = HttpUtility.HtmlDecode(inputString);

            return Regex.Replace
                (inputString, "<.*?>", string.Empty);
        }
    }
}