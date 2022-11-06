using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using malone.Core.Commons.Helpers.Extensions;
using malone.Core.IoC;
using malone.Core.Sample.AN.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.AN.SqlServer.mvc.Models
{
    public class TodoListViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Nombre de Lista")]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Pendientes")]
        public int Pending { get; set; }

        [DisplayName("Realizadas")]
        public int Done { get; set; }
        public List<TaskItemViewModel> Items { get; set; }


        public List<TaskItemViewModel> PendingItems(bool includeDeleted = false)
        {
            var items = new List<TaskItemViewModel>();
            if (this.Items != null)
            {
                items = this.Items.Where(i => !i.Done && i.IsDeleted == includeDeleted).ToList();
            }
            return items;
        }

        public List<TaskItemViewModel> DoneItems(bool includeDeleted = false)
        {
            var items = new List<TaskItemViewModel>();
            if (this.Items != null)
            {
                items = this.Items.Where(i => i.Done && i.IsDeleted == includeDeleted).ToList();
            }
            return items;
        }

        public static TodoListViewModel CreateFromEntity(TodoList list)
        {
            var mapper = ServiceLocator.Current.Get<Mapper>();
            return mapper.Map<TodoList, TodoListViewModel>(list);
        }
        public static IEnumerable<TodoListViewModel> CreateFromList(IEnumerable<TodoList> lists)
        {
            var mapper = ServiceLocator.Current.Get<Mapper>();
            return mapper.Map<IEnumerable<TodoList>, List<TodoListViewModel>>(lists);
        }
    }
}