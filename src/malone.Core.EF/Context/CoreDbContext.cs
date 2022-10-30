using System.Data.Entity;
using malone.Core.DataAccess.Context;

namespace malone.Core.EF.Context
{
	public abstract class CoreDbContext : DbContext, IContext
	{
		public CoreDbContext() : base()
		{ }

		public CoreDbContext(string connectionStringName) : base(connectionStringName)
		{
		}
	}
}
