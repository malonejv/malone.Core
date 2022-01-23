﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMS.Core.EL;
using GMS.Core.EL.Model;

namespace GMS.Core.DAL.Context.EF.Mappings
{
    public class TodoListMapping : EntityTypeConfiguration<TodoList>
    {
        public TodoListMapping()
        {
            ToTable("TODOLISTS");
            //Comentar ToTable y habilitar la linea de abajo para filtrar "siempre" los campos eliminados.
            //Map(m => m.ToTable("TodoLists").Requires("IsDeleted").HasValue(false)).Ignore(m => m.IsDeleted);

            HasKey(t => t.Id);

            Property(t => t.Name).HasMaxLength(100);
            Property(t => t.IsDeleted);

            HasMany(t => t.Items).WithRequired();
        }
    }
}
