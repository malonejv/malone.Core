﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.EL;
using malone.Core.EL.Model;

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
