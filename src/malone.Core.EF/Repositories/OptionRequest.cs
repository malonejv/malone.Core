using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Entities.Filters;

namespace malone.Core.EF.Repositories
{
	public class OptionRequest : IOptionRequest
	{
		public OptionRequest()
		{
			IncludeDeleted = false;
			IncludeProperties = "";
		}

		public bool IncludeDeleted { get; set; }
		public string IncludeProperties { get; set; }
	}
}
