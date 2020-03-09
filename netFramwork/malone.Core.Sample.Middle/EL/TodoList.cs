using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.EL;

namespace malone.Core.Sample.Middle.EL
{
    public class TodoList : IBaseEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskItem> Items { get; set; }
        public bool IsDeleted { get; set; }
    }
}
