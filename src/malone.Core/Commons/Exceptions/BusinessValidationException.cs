//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:52</date>

namespace malone.Core.Commons.Exceptions
{
	using System;

	/// <summary>
	/// Defines the <see cref="T: BusinessValidationException" />.
	/// </summary>
	public class BusinessValidationException : BaseException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T: BusinessValidationException"/> class.
		/// </summary>
		public BusinessValidationException()
: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T: BusinessValidationException"/> class.
		/// </summary>
		/// <param name="message">The message<see cref="T: string"/>.</param>
		public BusinessValidationException(string message)
: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T: BusinessValidationException"/> class.
		/// </summary>
		/// <param name="message">The message<see cref="T: string"/>.</param>
		/// <param name="innerException">The innerException<see cref="T: Exception"/>.</param>
		public BusinessValidationException(string message, Exception innerException)
: base(message, innerException)
		{
		}
	}
}
