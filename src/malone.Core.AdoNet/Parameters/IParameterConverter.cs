using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.AdoNet.Parameters
{
	public interface IParameterConverter
	{
		object Convert(object value);
	}
}
