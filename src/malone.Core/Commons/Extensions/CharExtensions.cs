//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:57</date>

namespace malone.Core.Commons.Helpers.Extensions
{
	/// <summary>
	/// Defines the <see cref="CharExtensions" />.
	/// </summary>
	public static class CharExtensions
	{
		/// <summary>
		/// Defines the DefaultSeparator.
		/// </summary>
		public const char DefaultSeparator = ';';

		/// <summary>
		/// The Concat.
		/// </summary>
		/// <param name="separator">The separator<see cref="char"/>.</param>
		/// <param name="parameters">The parameters<see cref="T: string[]"/>.</param>
		/// <returns>The <see cref="string"/>.</returns>
		public static string Concat(this char separator, params string[] parameters)
		{
			var result = "";

			foreach (var prm in parameters)
			{
				result += prm + separator;
			}

			result = result.Substring(0, result.Length - 1);

			return result;
		}

		/// <summary>
		/// The Split.
		/// </summary>
		/// <param name="separator">The separator<see cref="char"/>.</param>
		/// <param name="value">The value<see cref="string"/>.</param>
		/// <returns>The <see cref="T: string[]"/>.</returns>
		public static string[] Split(this char separator, string value)
		{
			string[] result = value.Split(separator);

			return result;
		}
	}
}
