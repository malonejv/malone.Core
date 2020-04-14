namespace malone.Core.Sample.Middle.DAL.Context.EF.Test
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<TASKITEMS> TASKITEMS { get; set; }
        public virtual DbSet<TODOLISTS> TODOLISTS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TASKITEMS>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TASKITEMS>()
                .Property(e => e.TODOLIST_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TODOLISTS>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TODOLISTS>()
                .HasMany(e => e.TASKITEMS)
                .WithRequired(e => e.TODOLISTS)
                .HasForeignKey(e => e.TODOLIST_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
