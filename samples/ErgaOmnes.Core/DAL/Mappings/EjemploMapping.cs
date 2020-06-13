using ErgaOmnes.Core.EL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgaOmnes.Core.DAL.Mappings
{
    public class EjemploMapping : EntityTypeConfiguration<Ejemplo>
    {
        public EjemploMapping()
        {
            ToTable("Ejemplos");
            
            HasKey(t => t.Id); //No Hace falta porque EF lo entiende por convención pero no está demás
            Property(t => t.Text).HasMaxLength(100);
        }
    }
}
