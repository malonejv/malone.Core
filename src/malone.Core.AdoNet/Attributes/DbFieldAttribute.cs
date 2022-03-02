namespace malone.Core.AdoNet.Attributes
{
	using System;

	/// <summary>
	/// Defines the <see cref="T: DbFieldAttribute" />.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class DbFieldAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T: DbFieldAttribute"/> class.
		/// </summary>
		/// <param name="name">The name<see cref="T: string"/>.</param>
		public DbFieldAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Gets or sets the Name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the ValueConverter.
		/// </summary>
		public Type ValueConverter { get; set; }
	}
}
