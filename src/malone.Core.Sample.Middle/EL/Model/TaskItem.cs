using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.EL;
using malone.Core.EL.Model;

namespace malone.Core.Sample.Middle.EL.Model
{
    public class TaskItem : IBaseEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
