using malone.Core.Entities.Model;
using malone.Core.Identity.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace malone.Core.Sample.EF.Firebird.Middle.EL.Model
{
    public class TodoList : IBaseEntity, ISoftDelete
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [DisplayName("Nombre de Lista")]
        [StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Fecha")]
        public DateTime? Date { get; set; }

        [DisplayName("Items")]
        public List<TaskItem> Items { get; set; }

        [DisplayName("Eliminado")]
        public bool IsDeleted { get; set; }

        public CoreUser User { get; set; }
    }
}
