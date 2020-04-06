using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.EL;

namespace malone.Core.Sample.Middle.EL
{
    public class TaskItem : IBaseEntity<decimal>, ISoftDelete
    {
        public decimal Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
