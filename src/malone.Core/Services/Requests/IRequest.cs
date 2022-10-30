using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Services.Requests
{
	public interface IRequest
	{

	}

	public interface IRequest<TEntity> : IRequest
		where TEntity : class
	{
		TEntity ToEntity();
	}
}
