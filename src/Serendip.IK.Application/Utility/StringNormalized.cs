using System;

namespace Serendip.IK.Utility
{
    public static class StringNormalized
    {
        public static string NormalizedString(this string str)
        {
 
            string text = str.ToUpper() 
                .Replace("İ", "I")
                .Replace("Ğ", "G")
                .Replace("Ş", "S")
                .Replace("Ö", "O")
                .Replace("Ü", "U")
                .Replace("Ç", "C")
                .Replace(".", " ")
                .Replace("-", " ");

            var result = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return string.Join("_", result);
        }
    }
}
