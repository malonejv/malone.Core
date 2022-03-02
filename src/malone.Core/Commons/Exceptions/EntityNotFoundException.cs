//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:53</date>

namespace malone.Core.Commons.Exceptions
{
	using System;

	/// <summary>
	/// Defines the <see cref="EntityNotFoundException" />.
	/// </summary>
	public class EntityNotFoundException : BaseException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
		/// </summary>
		public EntityNotFoundException()
: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		public EntityNotFoundException(string message)
: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
		/// </summary>
		/// <param name="message">The message<see cref="string"/>.</param>
		/// <param name="innerException">The innerException<see cref="Exception"/>.</param>
		public EntityNotFoundException(string message, Exception innerException)
: base(message, innerException)
		{
		}
	}
}
