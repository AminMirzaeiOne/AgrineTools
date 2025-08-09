using System;
using System.Text;

namespace AgrineCore.Practical
{
    public static class TextUtilities
    {
        public static string ToBase64(string plainText)
        {
            if (plainText == null) return null;
            var bytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(bytes);
        }

        public static string FromBase64(string base64Encoded)
        {
            if (base64Encoded == null) return null;
            try
            {
                var bytes = Convert.FromBase64String(base64Encoded);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return null;
            }
        }

        public static string ChangeEncoding(string text, Encoding fromEncoding, Encoding toEncoding)
        {
            if (text == null) return null;
            var fromBytes = fromEncoding.GetBytes(text);
            var toBytes = Encoding.Convert(fromEncoding, toEncoding, fromBytes);
            return toEncoding.GetString(toBytes);
        }

        public static string RemoveSpecialChars(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var sb = new StringBuilder();
            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
