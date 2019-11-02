using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ShoppingEcommerce.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ConvertToBytes(this string value)
        {
            return Encoding.Default.GetBytes(value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IReadOnlyList<string> SplitSqlStatementsByGoKeyword(this string value)
        {
            return Regex
                .Split(value
                    , @"^[\t\r\n]*GO[\t\r\n]*\d*[\t\r\n]*(?:--.*)?$"
                    , RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase)
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .ToList();
        }

        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return char.ToLower(value[0]) + value.Substring(1);
        }

        public static string RemoveDiacritics(this string text, bool removeWhitespace = false)
        {
            if (string.IsNullOrEmpty(text)) return text;
            
            var normalizedString = text.Normalize(NormalizationForm.FormD);

            var stringBuilder = new StringBuilder();

            foreach (var character in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);

                if (unicodeCategory == UnicodeCategory.NonSpacingMark)
                {
                    continue;
                }

                if (character.ToString() == "đ")
                {
                    stringBuilder.Append(Encoding
                        .UTF8.GetString(Encoding
                            .GetEncoding("ISO-8859-8")
                            .GetBytes(character.ToString())));
                }
                else
                {
                    stringBuilder.Append(character);
                }
            }

            return removeWhitespace 
                ? Regex.Replace(stringBuilder.ToString().Normalize(NormalizationForm.FormC), @"\s+", string.Empty) 
                : stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}