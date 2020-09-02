using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malone.Core.Sample.EF.Firebird.Middle.EL;
using malone.Core.Sample.EF.Firebird.Middle.EL.Model;

namespace malone.Core.Sample.EF.Firebird.Middle.DAL.Context.EF.Mappings
{
    public class TaskItemMapping : EntityTypeConfiguration<TaskItem>
    {
        public TaskItemMapping()
        {
            ToTable("TASKITEMS");
            //Comentar ToTable y habilitar la linea de abajo para filtrar "siempre" los campos eliminados.
            //Map(m => m.ToTable("TaskItems").Requires("IsDeleted").HasValue(false)).Ignore(m => m.IsDeleted);


            HasKey(t => t.Id);

            Property(t => t.Description).HasMaxLength(100);
            Property(t => t.IsDeleted);
        }
    }
}
