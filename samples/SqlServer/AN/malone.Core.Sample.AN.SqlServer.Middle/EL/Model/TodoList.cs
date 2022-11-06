using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using malone.Core.Entities.Model;
using malone.Core.Identity.AdoNet.SqlServer.Entities;

namespace malone.Core.Sample.AN.SqlServer.Middle.EL.Model
{
    public class TodoList : IBaseEntity, ISoftDelete
    {
        public TodoList() { }

        public TodoList(string name)
        {
            Name = name;
            IsDeleted = false;
            Date = DateTime.Today.Date;
        }

        public TodoList(string name, CoreUser user, List<TaskItem> items = null)
        {
            Name = name;
            Items = items ?? Enumerable.Empty<TaskItem>().ToList();
            User = user;
            IsDeleted = false;
            Date = DateTime.Today.Date;
        }

        public int Id { get; private set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(100)]
        public string Name { get; private set; }

        [DisplayName("Fecha")]
        public DateTime? Date { get; private set; }

        [DisplayName("Items")]
        public List<TaskItem> Items { get; private set; }

        [DisplayName("Eliminado")]
        public bool IsDeleted { get; private set; }

        public int UserId { get; private set; }
        public CoreUser User { get; private set; }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void AddItem(TaskItem item)
        {
            Items.Add(item);
        }

        public List<TaskItem> PendingItems(bool includeDeleted = false)
        {
            var items = new List<TaskItem>();
            if (this.Items != null)
            {
                items = this.Items.Where(i => !i.Done && i.IsDeleted == includeDeleted).ToList();
            }
            return items;
        }

        public List<TaskItem> DoneItems(bool includeDeleted = false)
        {
            var items = new List<TaskItem>();
            if (this.Items != null)
            {
                items = this.Items.Where(i => i.Done && i.IsDeleted == includeDeleted).ToList();
            }
            return items;
        }
    }
}
