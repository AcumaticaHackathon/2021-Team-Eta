
using System;

namespace JsonConfigurator
{
    public static class Extensions
    {
        public static string EscapeJson(this string text)
        {
            //Backspace to be replaced with \b.
            //    Form feed to be replaced with \f.

            //    Carriage return to be replaced with \r.

           


            return text.Replace("\n", @"\n")            //    Newline to be replaced with \n.
                .Replace("\t", @"\t")             //    Tab to be replaced with \t.
                .Replace("\"", "\\" + "\"") //    Double quote to be replaced with \"
                .Replace("\\", @"\\");            //Backslash to be replaced with \\
        }

        public static bool IsUiView(this string viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName)) return false;
            return
                !viewName.StartsWith("_")
                && !viewName.StartsWith("$")
                && char.IsUpper(viewName[0])
                && !viewName.StartsWith("Current");
        }
    }
}