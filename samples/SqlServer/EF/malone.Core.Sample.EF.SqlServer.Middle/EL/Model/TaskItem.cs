using malone.Core.Entities.Model;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace malone.Core.Sample.EF.SqlServer.Middle.EL.Model
{
    public class TaskItem : IBaseEntity, ISoftDelete
    {
        public TaskItem() { }

        public TaskItem(string description) {
            Description = description;
        }


        [ScaffoldColumn(false)]
        public int Id { get; private set; }

        [Required(ErrorMessage = "El campo descripción es requerido")]
        [StringLength(100)]
        public string Description { get; private set; }

        [DefaultValue(false)]
        public bool Done { get; private set; }

        public bool IsDeleted { get; private set; }

        public void ToggleDone()
        {
            Done = !Done;
        }
    }
}
