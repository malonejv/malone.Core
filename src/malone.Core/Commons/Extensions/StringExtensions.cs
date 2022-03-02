//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:59</date>

namespace malone.Core.Commons.Helpers.Extensions
{
	using System;
	using System.Runtime.InteropServices;
	using System.Security;
	using System.Text;
	using System.Text.RegularExpressions;

	/// <summary>
	/// Defines the <see cref="T: StringExtensions" />.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// The ToByteArray.
		/// </summary>
		/// <param name="hex">The hex<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: byte[]"/>.</returns>
		public static byte[] ToByteArray(this string hex)
		{
			int NumberChars = hex.Length;
			byte[] bytes = new byte[NumberChars / 2];
			for (int i = 0; i < NumberChars; i += 2)
			{
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			}

			return bytes;
		}

		/// <summary>
		/// The ToSecureString.
		/// </summary>
		/// <param name="source">The source<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: SecureString"/>.</returns>
		public static SecureString ToSecureString(this string source)
		{
			if (string.IsNullOrWhiteSpace(source))
			{
				return null;
			}

			SecureString secureString = new SecureString();
			foreach (char c in source)
			{
				secureString.AppendChar(c);
			}

			return secureString;
		}

		/// <summary>
		/// The SecureStringToString.
		/// </summary>
		/// <param name="value">The value<see cref="T: SecureString"/>.</param>
		/// <returns>The <see cref="T: string"/>.</returns>
		public static string SecureStringToString(this SecureString value)
		{
			if (value == null)
			{
				return null;
			}

			IntPtr ptr = IntPtr.Zero;
			try
			{
				ptr = Marshal.SecureStringToBSTR(value);
				return Marshal.PtrToStringBSTR(ptr);
			}
			finally
			{
				if (ptr != IntPtr.Zero)
				{
					Marshal.FreeBSTR(ptr);
				}
			}
		}

		/// <summary>
		/// The IsNullOrEmpty.
		/// </summary>
		/// <param name="text">The text<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: bool"/>.</returns>
		public static bool IsNullOrEmpty(this string text)
		{
			return string.IsNullOrEmpty(text);
		}

		/// <summary>
		/// The IsNotNullOrEmpty.
		/// </summary>
		/// <param name="text">The text<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: bool"/>.</returns>
		public static bool IsNotNullOrEmpty(this string text)
		{
			return !string.IsNullOrEmpty(text);
		}

		/// <summary>
		/// The ThrowIfNullOrEmpty.
		/// </summary>
		/// <param name="text">The text<see cref="T: string"/>.</param>
		/// <param name="paramName">The paramName<see cref="T: string"/>.</param>
		public static void ThrowIfNullOrEmpty(this string text, string paramName)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException(paramName, $"Parameter {paramName} cannot be null.");
			}
		}

		/// <summary>
		/// The EncodeToISO88598.
		/// </summary>
		/// <param name="inputText">The inputText<see cref="T: string"/>.</param>
		/// <param name="initialEncoding">The initialEncoding<see cref="T: Encoding"/>.</param>
		/// <returns>The <see cref="T: string"/>.</returns>
		public static string EncodeToISO88598(this string inputText, Encoding initialEncoding = null)
		{
			Encoding iso88598 = Encoding.GetEncoding("iso-8859-8");
			if (initialEncoding == null)
			{
				initialEncoding = Encoding.GetEncoding("iso-8859-1");
			}

			byte[] textBytes = initialEncoding.GetBytes(inputText.Trim());
			byte[] textConvertedBytes = Encoding.Convert(initialEncoding, iso88598, textBytes);

			char[] convertedChars = new char[iso88598.GetCharCount(textConvertedBytes, 0, textConvertedBytes.Length) - 1 + 1];
			iso88598.GetChars(textConvertedBytes, 0, textConvertedBytes.Length, convertedChars, 0);
			string result = new string(convertedChars);

			return result;
		}

		/// <summary>
		/// The EncodeAndRemoveSpecialCharacters.
		/// </summary>
		/// <param name="inputText">The inputText<see cref="T: string"/>.</param>
		/// <param name="initialEncoding">The initialEncoding<see cref="T: Encoding"/>.</param>
		/// <param name="replacementChar">The replacementChar<see cref="T: char"/>.</param>
		/// <returns>The <see cref="T: string"/>.</returns>
		public static string EncodeAndRemoveSpecialCharacters(this string inputText, Encoding initialEncoding = null, char replacementChar = ' ')
		{
			string result = inputText.EncodeToISO88598(initialEncoding);

			result = result.RemoveSpecialCharacters(replacementChar);

			return result;
		}

		/// <summary>
		/// The RemoveSpecialCharacters.
		/// </summary>
		/// <param name="inputText">The inputText<see cref="T: string"/>.</param>
		/// <param name="replacementChar">The replacementChar<see cref="T: char"/>.</param>
		/// <returns>The <see cref="T: string"/>.</returns>
		public static string RemoveSpecialCharacters(this string inputText, char replacementChar = ' ')
		{
			inputText = inputText.Trim();

			Regex regEx = new Regex("(?![a-zA-Z0-9 ]).");
			MatchCollection matches = regEx.Matches(inputText);

			foreach (Match item in matches)
			{
				inputText = inputText.Replace(item.Value, replacementChar.ToString());
			}

			return inputText;
		}

		/// <summary>
		/// The HasSpecialCharacters.
		/// </summary>
		/// <param name="inputText">The inputText<see cref="T: string"/>.</param>
		/// <returns>The <see cref="T: bool"/>.</returns>
		public static bool HasSpecialCharacters(this string inputText)
		{
			inputText = inputText.Trim();

			Regex regEx = new Regex("(?![a-zA-Z0-9 ]).");
			MatchCollection matches = regEx.Matches(inputText);

			return matches.Count > 0;
		}
	}
}
