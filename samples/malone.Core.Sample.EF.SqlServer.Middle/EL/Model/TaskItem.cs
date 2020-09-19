using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using malone.Core.Entities.Model;

namespace malone.Core.Sample.EF.SqlServer.Middle.EL.Model
{
    public class TaskItem : IBaseEntity, ISoftDelete
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo descripción es requerido")]
        [DisplayName("Descripción")]
        [StringLength(100)]
        public string Description { get; set; }

        [DisplayName("Pendiente")]
        [DefaultValue(false)]
        public bool Done { get; set; }

        [DisplayName("Eliminado")]
        public bool IsDeleted { get; set; }
    }
}
