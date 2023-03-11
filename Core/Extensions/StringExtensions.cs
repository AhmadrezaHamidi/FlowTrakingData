using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Core
{
    public static class StringExtensions
    {
        private static readonly Regex _matchAllTags =
            new Regex(@"<(.|\n)*?>", options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex _matchArabicHebrew =
           new Regex(@"[\u0600-\u06FF,\u0590-\u05FF]", options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private const char RleChar = (char)0x202B;

        /// <summary>
        /// convert Base64String to byte array.
        /// </summary>
        public static byte[] ToByteArray(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return new byte[2];
            else
            {
                var bytes = Convert.FromBase64String(str);

                return bytes;
            }
        }

        /// <summary>
        /// Is string contains farsi character?
        /// </summary>
        /// <returns>return true if string is contains farsi character.otherwise return false.</returns>
        public static bool ContainsFarsi(this string txt)
        {
            return !string.IsNullOrEmpty(txt) &&
                _matchArabicHebrew.IsMatch(txt.StripHtmlTags().Replace(",", ""));
        }

        /// <summary>
        /// Remove HTML tags from string.
        /// </summary>
        public static string StripHtmlTags(this string text)
        {
            return string.IsNullOrEmpty(text) ?
                        string.Empty :
                        _matchAllTags.Replace(text, " ").Replace("&nbsp;", " ");
        }

        /// <summary>
        /// if string comtains farsi words wraped with rtl div tag.
        /// </summary>
        public static string WrapInDirectionalDiv(this string body, string fontFamily = "tahoma", string fontSize = "9pt")
        {
            if (string.IsNullOrWhiteSpace(body))
                return string.Empty;

            if (ContainsFarsi(body))
                return $"<div style='text-align: right; font-family:{fontFamily}; font-size:{fontSize};' dir='rtl'>{body}</div>";
            return $"<div style='text-align: left; font-family:{fontFamily}; font-size:{fontSize};' dir='ltr'>{body}</div>";
        }

        /// <summary>
        ///  Applies RLE to the text if it contains Persian words.
        /// </summary>
        public static string ApplyRle(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            return text.ContainsFarsi() ? $"{RleChar}{text}" : text;
        }

        /// <summary>
        /// Remove diacritics from text
        /// </summary>
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormKC);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// remove underlines from text.
        /// </summary>
        public static string CleanUnderLines(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            const char chr1600 = (char)1600; //ـ=1600
            const char chr8204 = (char)8204; //‌=8204

            return text.Replace(chr1600.ToString(), "")
                       .Replace(chr8204.ToString(), "");
        }

        /// <summary>
        /// remove punctuation from text.
        /// </summary>
        public static string RemovePunctuation(this string text)
        {
            return string.IsNullOrWhiteSpace(text) ?
                string.Empty :
                new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
        }

        /// <summary>
        /// check is null or empty string
        /// </summary>
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static string ConvertPersianDigits(this string text)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(text) || String.IsNullOrEmpty(text))
                    return "";

                var textStr = StripHTML(text);

                return textStr.Replace("٠", "0").Replace("۰", "0")
                    .Replace("١", "1").Replace("۱", "1")
                    .Replace("٢", "2").Replace("۲", "2")
                    .Replace("٣", "3").Replace("۳", "3")
                    .Replace("٤", "4").Replace("۴", "4")
                    .Replace("٥", "5").Replace("۵", "5")
                    .Replace("٦", "6").Replace("۶", "6")
                    .Replace("٧", "7").Replace("۷", "7")
                    .Replace("٨", "8").Replace("۸", "8")
                    .Replace("٩", "9").Replace("۹", "9");
            }
            catch
            {
                return text;
            }
        }

        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string CleanIranMobilePhoneNumber(this string mobileNumber)
        {
            var result = mobileNumber;
            try
            {
                result = result.Trim().Replace(" ", "").Replace("-", "").ConvertPersianDigitsAndArabicAlphebet();
                if (result.StartsWith("+98"))
                    result = result.Replace("+98", "0");
                if (result.StartsWith("0098"))
                    result = "+" + result.Replace("+0098", "0");
                if (result.StartsWith("98"))
                    result = "+00" + result.Replace("+0098", "0");
                if (result.StartsWith("00"))
                    result = "+" + result.Replace("+00", "0");
                if (!result.IsNumberOnly())
                    result = result.OnlyNumberAccept();
            }
            catch
            {
            }
            return result;
        }

        public static string ConvertPersianDigitsAndArabicAlphebet(this string temp)
        {
            try
            {
                var result = temp.Trim().ConvertPersianDigits();
                result = result.Replace("ي", "ی").Replace("ك", "ک");
                return result;
            }
            catch
            {
                return temp;
            }
        }

        public static bool IsNumberOnly(this string numberStr, bool emptyStringIsNumber = true)
        {
            if (String.IsNullOrWhiteSpace(numberStr) || String.IsNullOrEmpty(numberStr))
                return emptyStringIsNumber;

            var numberCharArray = "0123456789".ToCharArray();
            var charArray = numberStr.Trim().ToCharArray();
            foreach (var charData in charArray)
                if (!numberCharArray.Contains(charData))
                    return false;

            return true;
        }

        public static string OnlyNumberAccept(this string str)
        {
            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str))
                return "";

            var numberCharArray = "0123456789".ToCharArray();
            var charArray = str.ConvertPersianDigits().ToCharArray();
            var result = "";
            foreach (var charData in charArray)
                if (numberCharArray.Contains(charData))
                    result += charData;

            return result;
        }
    }
}