using malone.Core.Dapper.Attributes;
using malone.Core.Entities.Model;
using malone.Core.Identity.Dapper.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace malone.Core.Sample.AdoNet.SqlServer.Middle.EL.Model
{
    [Table("TodoLists")]
    public class TodoList : IBaseEntity, ISoftDelete
    {
        [ScaffoldColumn(false)]
        [Column("Id", DbType.Int32, Direction = ParameterDirection.Input)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [DisplayName("Nombre de Lista")]
        [StringLength(100)]
        [Column("Name", DbType.String, Size = 100, Direction = ParameterDirection.Input)]
        public string Name { get; set; }

        [DisplayName("Fecha")]
        [Column("Date", DbType.DateTime, Direction = ParameterDirection.Input)]
        public DateTime? Date { get; set; }

        [DisplayName("Eliminado")]
        [Column("IsDeleted", DbType.Boolean, Direction = ParameterDirection.Input)]
        public bool IsDeleted { get; set; }

        [DisplayName("Items")]
        public List<TaskItem> Items { get; set; }

        [Column("UserId", DbType.Int32, Direction = ParameterDirection.Input)]
        public CoreUser User { get; set; }
    }
}
