using malone.Core.AdoNet.Attributes;
using malone.Core.Entities.Model;
using malone.Core.Identity.AdoNet.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model
{
    public class TodoList : IBaseEntity, ISoftDelete
    {
        [ScaffoldColumn(false)]
        [DbParameter("@Id", Type = SqlDbType.Int, Direction = ParameterDirection.Input)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [DisplayName("Nombre de Lista")]
        [StringLength(100)]
        [DbParameter("@Name", Type = SqlDbType.NVarChar, Size = 100, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

        [DisplayName("Fecha")]
        [DbParameter("@Date", Type = SqlDbType.DateTime, Direction = ParameterDirection.Input)]
        public DateTime? Date { get; set; }

        [DisplayName("Eliminado")]
        [DbParameter("@IsDeleted", Type = SqlDbType.Bit, Direction = ParameterDirection.Input)]
        public bool IsDeleted { get; set; }

        [DisplayName("Items")]
        public List<TaskItem> Items { get; set; }

        public CoreUser User { get; set; }
    }
}
