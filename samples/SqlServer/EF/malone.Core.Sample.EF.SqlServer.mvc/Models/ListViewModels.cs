using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace malone.Core.Sample.EF.SqlServer.mvc.Models
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
        public List<TaskItem> Items { get; set; }

    }

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
    }

    public class ListIndexViewModel
    {
        public IEnumerable<TodoListViewModel> Listas { get; set; }

        public TodoListViewModel NuevaLista { get; set; }

        public TodoListViewModel EliminarLista { get; set; }

        public TodoListViewModel EditarLista { get; set; }
    }

    public class ListDetailsViewModel
    {
        public TodoListViewModel Lista { get; set; }

        public TaskItemViewModel NuevaTarea { get; set; }

        public TaskItemViewModel EliminarTarea { get; set; }

        public TaskItemViewModel EditarTarea { get; set; }
    }
}