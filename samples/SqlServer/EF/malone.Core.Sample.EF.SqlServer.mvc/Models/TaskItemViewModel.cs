using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using malone.Core.IoC;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;

namespace malone.Core.Sample.EF.SqlServer.mvc.Models
{
    public class TaskItemViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(100)]
        public string Description { get; set; }

        [DisplayName("Pendiente")]
        [DefaultValue(false)]
        public bool Done { get; set; }

        [DisplayName("Eliminado")]
        public bool IsDeleted { get; set; }

        public int ListId { get; set; }

        public static TaskItemViewModel CreateFromEntity(int listId, TaskItem item)
        {
            var mapper = ServiceLocator.Current.Get<Mapper>();
            var taskItem = mapper.Map<TaskItem, TaskItemViewModel>(item);
            taskItem.ListId = listId;
            return taskItem;
        }
        public static IEnumerable<TaskItemViewModel> CreateFromList(int listId, IEnumerable<TaskItem> items)
        {
            var mapper = ServiceLocator.Current.Get<Mapper>();
            var taskItems= mapper.Map<IEnumerable<TaskItem>, List<TaskItemViewModel>>(items);
            taskItems.ForEach(i => i.ListId = listId);
            return taskItems;
        }
    }
}