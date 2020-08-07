using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace malone.Core.Commons.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        #region Special Characters
        //::::::::::::::::::::::::
        //source: https://www.codeproject.com/Articles/18879/Encoding-Accented-Characters
        //::::::::::::::::::::::::


        /// <summary>
        ///Encodes the text in ISO-8859-8, the encoding of the input text must correspond to the Encoding parameter.
        ///If the initialEncoding parameter is null, by default the ISO-8859-1 encoding is used.
        ///</summary>
        ///<param name="inputText"></param>
        ///<returns></returns>
        public static string EncodeToISO88598(this string inputText, Encoding initialEncoding = null)
        {
            Encoding iso88598 = Encoding.GetEncoding("iso-8859-8");
            if (initialEncoding == null)
                initialEncoding = Encoding.GetEncoding("iso-8859-1");

            byte[] textBytes = initialEncoding.GetBytes(inputText.Trim());
            byte[] textConvertedBytes = Encoding.Convert(initialEncoding, iso88598, textBytes);

            char[] convertedChars = new char[iso88598.GetCharCount(textConvertedBytes, 0, textConvertedBytes.Length) - 1 + 1];
            iso88598.GetChars(textConvertedBytes, 0, textConvertedBytes.Length, convertedChars, 0);
            string result = new string(convertedChars);

            return result;
        }

        /// <summary>
        ///Encodes text in ISO-8859-8 and removes special characters.
        ///If the initialEncoding parameter is null, by default the ISO-8859-1 encoding is used.
        ///The input text must correspond to the Encoding parameter.
        ///</summary>
        ///<param name="inputText"></param>
        ///<param name="initialEncoding"></param>
        ///<param name="replacementChar"></param>
        ///<returns></returns>
        public static string EncodeAndRemoveSpecialCharacters(this string inputText, Encoding initialEncoding = null, char replacementChar = ' ')
        {
            string result = inputText.EncodeToISO88598(initialEncoding);

            result = result.RemoveSpecialCharacters(replacementChar);

            return result;
        }

        /// <summary>
        ///Removes special characters.
        ///</summary>
        ///<param name="inputText"></param>
        ///<param name="replacementChar"></param>
        ///<returns></returns>
        public static string RemoveSpecialCharacters(this string inputText, char replacementChar = ' ')
        {
            inputText = inputText.Trim();

            Regex regEx = new Regex("(?![a-zA-Z0-9 ]).");
            MatchCollection matches = regEx.Matches(inputText);

            foreach (Match item in matches)
                inputText = inputText.Replace(item.Value, replacementChar.ToString());

            return inputText;
        }

        /// <summary>
        ///Validates whether an input text has special characters.
        ///</summary>
        ///<param name="inputText"></param>
        ///<returns></returns>
        public static bool HasSpecialCharacters(this string inputText)
        {
            inputText = inputText.Trim();

            Regex regEx = new Regex("(?![a-zA-Z0-9 ]).");
            MatchCollection matches = regEx.Matches(inputText);

            return matches.Count > 0;
        }

        #endregion
    }

}
