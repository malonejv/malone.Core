using malone.Core.Identity.EntityFramework.Entities;
using malone.Core.Sample.EF.SqlServer.Middle.EL.Model;
using System.Data.Entity.ModelConfiguration;

namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Context.Mappings
{
    public class TodoListMapping : EntityTypeConfiguration<TodoList>
    {
        public TodoListMapping()
        {
            ToTable("TodoLists");
            //Comentar ToTable y habilitar la linea de abajo para filtrar "siempre" los campos eliminados.
            //Map(m => m.ToTable("TodoLists").Requires("IsDeleted").HasValue(false)).Ignore(m => m.IsDeleted);

            HasKey(t => t.Id);

            Property(t => t.Name).HasMaxLength(100);
            Property(t => t.IsDeleted);

            HasMany(t => t.Items).WithRequired();

            HasRequired(t=>t.User).WithMany().HasForeignKey(t => t.UserId);
        }
    }
}
