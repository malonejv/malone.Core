using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Entities.Model
{
    public interface IDateRange
    {
        DateTime? FromDate { get; set; }
        DateTime? ToDate { get; set; }
    }

}
