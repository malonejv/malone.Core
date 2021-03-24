using FirebirdSql.Data.FirebirdClient;
using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model
{
    public class TaskItem : IBaseEntity, ISoftDelete
    {
        [ScaffoldColumn(false)]
        [DbParameter("@Id", Type = FbDbType.Integer, Direction = ParameterDirection.Input)]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo descripción es requerido")]
        [DisplayName("Descripción")]
        [StringLength(100)]
        [DbParameter("@Name", Type = FbDbType.VarChar, Size = 100, Direction = ParameterDirection.Input)]
        public string Description { get; set; }

        [DisplayName("Pendiente")]
        [DefaultValue(false)]
        [DbParameter("@Done", Type = FbDbType.SmallInt, Direction = ParameterDirection.Input)]
        public bool Done { get; set; }

        [DisplayName("Eliminado")]
        [DbParameter("@IsDeleted", Type = FbDbType.SmallInt, Direction = ParameterDirection.Input)]
        public bool IsDeleted { get; set; }
    }
}
