using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model
{
    public class TaskItem : IBaseEntity, ISoftDelete
    {
        [ScaffoldColumn(false)]
        [DbParameter("@Id", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo descripción es requerido")]
        [DisplayName("Descripción")]
        [StringLength(100)]
        [DbParameter("@Name", Type = SqlDbType.NVarChar, Size = 100, Direction = ParameterDirection.Input)]
        public string Description { get; set; }

        [DisplayName("Pendiente")]
        [DefaultValue(false)]
        [DbParameter("@Done", Type = SqlDbType.Bit, Direction = ParameterDirection.Input)]
        public bool Done { get; set; }

        [DisplayName("Eliminado")]
        [DbParameter("@IsDeleted", Type = SqlDbType.Bit, Direction = ParameterDirection.Input)]
        public bool IsDeleted { get; set; }
    }
}
