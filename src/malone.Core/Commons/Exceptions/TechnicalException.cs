//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:56</date>

namespace malone.Core.Commons.Exceptions
{
	using System;

	/// <summary>
	/// Defines the <see cref="TechnicalException" />.
	/// </summary>
	public class TechnicalException : BaseException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TechnicalException"/> class.
		/// </summary>
		public TechnicalException()
: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TechnicalException"/> class.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		public TechnicalException(string message)
: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TechnicalException"/> class.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		/// <param name="innerException">The innerException<see cref="Exception"/>.</param>
		public TechnicalException(string message, Exception innerException)
: base(message, innerException)
		{
		}
	}
}
