using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace EmailSender.Extensions
{
    // jak chcemy dodać metodę rozszerzjącą to klasa musi być statyczna
    // metoda musi być statyczna
    // poprzez słowo kluczowe this wskazujamy jaką metodę rozszerzamy
    public static class StringExtentions
    {
        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
