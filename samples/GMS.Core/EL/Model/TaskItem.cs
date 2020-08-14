﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using malone.Core.Entities.Model;

namespace GMS.Core.EL.Model
{
    public class TaskItem : IBaseEntity, ISoftDelete
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo descripción es requerido")]
        [DisplayName("Descripción")]
        [StringLength(100)]
        public string Description { get; set; }

        [DisplayName("Eliminado")]
        public bool IsDeleted { get; set; }
    }
}