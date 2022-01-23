using malone.Core.Dapper.Attributes;
using malone.Core.Entities.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model
{
    [Table("TaskItems")]
    public class TaskItem : IBaseEntity, ISoftDelete
    {
        [ScaffoldColumn(false)]
        [Column("Id", DbType.Int32, Direction = ParameterDirection.Input)]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo descripción es requerido")]
        [DisplayName("Descripción")]
        [StringLength(100)]
        [Column("Name", DbType.String, Size = 100, Direction = ParameterDirection.Input)]
        public string Description { get; set; }

        [DisplayName("Pendiente")]
        [DefaultValue(false)]
        [Column("Done", DbType.Boolean, Direction = ParameterDirection.Input)]
        public bool Done { get; set; }

        [DisplayName("Eliminado")]
        [Column("IsDeleted", DbType.Boolean, Direction = ParameterDirection.Input)]
        public bool IsDeleted { get; set; }
    }
}
