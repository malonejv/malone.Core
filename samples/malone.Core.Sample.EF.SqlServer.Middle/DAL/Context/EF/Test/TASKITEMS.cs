namespace malone.Core.Sample.EF.SqlServer.Middle.DAL.Context.EF.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SAMPLEUSER.TASKITEMS")]
    public partial class TASKITEMS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        [Required]
        [StringLength(100)]
        public string DESCRIPCION { get; set; }

        public bool ISDELETED { get; set; }

        public decimal TODOLIST_ID { get; set; }

        public virtual TODOLISTS TODOLISTS { get; set; }
    }
}
