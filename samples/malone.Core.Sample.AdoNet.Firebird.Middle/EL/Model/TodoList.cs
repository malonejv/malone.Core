using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;

namespace malone.Core.Sample.AdoNet.Firebird.Middle.EL.Model
{
    public class TodoList : IBaseEntity, ISoftDelete
    {
        [ScaffoldColumn(false)]
        [DbParameter("@Id", Type = FbDbType.Integer, Direction = ParameterDirection.Input)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [DisplayName("Nombre de Lista")]
        [StringLength(100)]
        [DbParameter("@Name", Type = FbDbType.VarChar, Size = 100, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

        [DisplayName("Fecha")]
        [DbParameter("@Date", Type = FbDbType.Date, Direction = ParameterDirection.Input)]
        public DateTime? Date { get; set; }

        [DisplayName("Eliminado")]
        [DbParameter("@IsDeleted", Type = FbDbType.SmallInt, Direction = ParameterDirection.Input)]
        public bool IsDeleted { get; set; }

        [DisplayName("Items")]
        public List<TaskItem> Items { get; set; }

    }
}
