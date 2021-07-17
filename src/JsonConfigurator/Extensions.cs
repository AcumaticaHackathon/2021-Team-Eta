#region #Copyright
//  ----------------------------------------------------------------------------------
// COPYRIGHT (c) 2021 CONTOU CONSULTING
// ALL RIGHTS RESERVED
// AUTHOR: Kyle Vanderstoep
// CREATED DATE: 2021/07/17
// ----------------------------------------------------------------------------------
#endregion

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
    }
}