using malone.Core.Entities.Model;
using System;
using System.Collections.Generic;

namespace malone.Core.Sample.Middle.EL.Model
{
    public class TodoList : IBaseEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public List<TaskItem> Items { get; set; }
        public bool IsDeleted { get; set; }
    }
}
