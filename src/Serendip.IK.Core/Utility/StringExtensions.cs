using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtensions
    {
        public static bool HasValue(this string str)
        {
            return !String.IsNullOrEmpty(str);
        }

        public static string GetFormattedPhoneNumber(this string val)
        {
            if (string.IsNullOrEmpty(val))
                return val;

            string phoneFormat = "+(0##) ### ###-####";

            // First, remove everything except of numbers
            Regex regexObj = new Regex(@"[^\d]");
            val = regexObj.Replace(val, "");

            // Second, format numbers to phone string
            if (val.Length > 0)
            {
                val = Convert.ToInt64(val).ToString(phoneFormat);
            }

            return val;
        }

        public static string NonFormattedPhoneNumber(this string val)
        {
            if (string.IsNullOrEmpty(val))
                return val;

            val = val.Trim().Replace("(", "").Replace(")", "").Replace("+", "").Replace("-", "").Replace(" ","");

            return val;
        }

        public static string CapitalizeFirst(this string s)
        {
            string[] sentence = s.Trim().Split(" ");

            if (sentence.Length == 1)
            {
                return sentence[0].First().ToString().ToUpper();
            }
            else
            {
                var result = sentence[0].First().ToString();
                foreach (var item in sentence.Reverse())
                {
                    if (String.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    result += item.First().ToString();
                    break;
                }
                return result.ToUpper();
            }
        }
    }
}
