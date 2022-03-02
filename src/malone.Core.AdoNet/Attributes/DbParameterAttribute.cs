namespace malone.Core.AdoNet.Attributes
{
	using System;
	using System.Data;

	/// <summary>
	/// Defines the <see cref="T: DbParameterAttribute" />.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class DbParameterAttribute : DbFieldAttribute
	{
		/// <summary>
		/// Defines the _size.
		/// </summary>
		private int _size;

		/// <summary>
		/// Initializes a new instance of the <see cref="T: DbParameterAttribute"/> class.
		/// </summary>
		/// <param name="name">The name<see cref="T: string"/>.</param>
		public DbParameterAttribute(string name) : base(name)
		{
		}

		/// <summary>
		/// Gets or sets the Order.
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		/// Gets or sets the Direction.
		/// </summary>
		public ParameterDirection Direction { get; set; }

		/// <summary>
		/// Gets or sets the Type.
		/// </summary>
		public object Type { get; set; }

		/// <summary>
		/// Gets a value indicating whether IsSizeDefined.
		/// </summary>
		public bool IsSizeDefined { get; private set; }

		/// <summary>
		/// Gets or sets the Size.
		/// </summary>
		public int Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
				this.IsSizeDefined = true;
			}
		}
	}
}
