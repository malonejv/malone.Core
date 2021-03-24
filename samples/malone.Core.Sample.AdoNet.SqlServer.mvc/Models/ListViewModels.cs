using malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model;
using System.Collections.Generic;

namespace malone.Core.Sample.AdoNet.SqlServer.mvc.Models
{
    public class ListIndexViewModel
    {
        public IEnumerable<TodoList> Listas { get; set; }

        public TodoList NuevaLista { get; set; }

        public TodoList EliminarLista { get; set; }

        public TodoList EditarLista { get; set; }
    }

    public class ListDetailsViewModel
    {
        public TodoList Lista { get; set; }

        public TaskItem NuevaTarea { get; set; }

        public TaskItem EliminarTarea { get; set; }

        public TaskItem EditarTarea { get; set; }
    }
}