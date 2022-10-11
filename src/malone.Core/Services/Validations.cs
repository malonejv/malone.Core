//<author>Javier López Malone</author>
//<date>25/11/2020 02:47:41</date>

namespace malone.Core.Services
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// The ValidationRuleDelegate.
	/// </summary>
	/// <param name="arguments">The arguments<see cref="object[]"/>.</param>
	/// <returns>The <see cref="ValidationResult"/>.</returns>
	public delegate ValidationResult ValidationRuleDelegate(params object[] arguments);

	/// <summary>
	/// Defines the <see cref="ValidationRule" />.
	/// </summary>
	public class ValidationRule
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationRule"/> class.
		/// </summary>
		public ValidationRule()
		{
			Arguments = new List<object>();
		}

		/// <summary>
		/// Gets or sets the Method.
		/// </summary>
		public ValidationRuleDelegate Method { get; set; }

		/// <summary>
		/// Gets or sets the Arguments.
		/// </summary>
		public List<object> Arguments { get; set; }
	}

	/// <summary>
	/// Defines the <see cref="ValidationResult" />.
	/// </summary>
	public class ValidationResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationResult"/> class.
		/// </summary>
		/// <param name="errorCode">The errorCode<see cref="string"/>.</param>
		/// <param name="message">The message<see cref="string"/>.</param>
		public ValidationResult(string errorCode, string message)
		{
			ErrorCode = errorCode;
			Message = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationResult"/> class.
		/// </summary>
		/// <param name="errorCode">The errorCode<see cref="string"/>.</param>
		public ValidationResult(string errorCode) : this(errorCode, null)
		{
		}

		/// <summary>
		/// Gets or sets the Return.
		/// </summary>
		public object Return { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether IsValid.
		/// </summary>
		public bool IsValid { get; set; }

		/// <summary>
		/// Gets or sets the ErrorCode.
		/// </summary>
		public string ErrorCode { get; set; }

		/// <summary>
		/// Gets or sets the Message.
		/// </summary>
		public string Message { get; internal protected set; }
	}

	/// <summary>
	/// Defines the <see cref="ValidationResultList" />.
	/// </summary>
	public class ValidationResultList : List<ValidationResult>
	{
		/// <summary>
		/// Gets a value indicating whether IsValid.
		/// </summary>
		public bool IsValid
		{
			get
			{
				if (this.Count > 0 && this.Any(item => item.IsValid == false))
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationResultList"/> class.
		/// </summary>
		public ValidationResultList()
		{
		}
	}
}
