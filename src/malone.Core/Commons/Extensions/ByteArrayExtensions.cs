//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:56</date>

namespace malone.Core.Commons.Extensions
{
	using System.Text;

	/// <summary>
	/// Defines the <see cref="ByteArrayExtensions" />.
	/// </summary>
	public static class ByteArrayExtensions
	{
		/// <summary>
		/// The ToHexString.
		/// </summary>
		/// <param name="ba">The ba<see cref="byte[]"/>.</param>
		/// <returns>The <see cref="string"/>.</returns>
		public static string ToHexString(this byte[] ba)
		{
			StringBuilder hex = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				hex.AppendFormat("{0:x2}", b);
			}

			return hex.ToString();
		}
	}
}
