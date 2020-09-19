using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Sample.EF.SqlServer.Middle.EL.Model.Extensions
{
    public static class TodoListExtensions
    {
        public static List<TaskItem> PendingItems(this TodoList list, bool includeDeleted = false)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));

            List<TaskItem> items = new List<TaskItem>();
            if (list.Items != null)
            {
                items = list.Items.Where(i => !i.Done && i.IsDeleted == includeDeleted).ToList();
            }
            return items;
        }
        public static List<TaskItem> DoneItems(this TodoList list, bool includeDeleted = false)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));

            List<TaskItem> items = new List<TaskItem>();
            if (list.Items != null)
            {
                items = list.Items.Where(i => i.Done && i.IsDeleted == includeDeleted).ToList();
            }
            return items;
        }
    }
}
