using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.EL.Model
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }

}
