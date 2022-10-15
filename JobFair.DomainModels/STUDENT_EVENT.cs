namespace JobFair.DomainModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class STUDENT_EVENT
    {
        [Key]
        public int IdReg { get; set; }

        public int IdEvent { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        [StringLength(50)]
        public string StudentName { get; set; }

        [StringLength(50)]
        public string StudentClass { get; set; }

        [StringLength(50)]
        public string StudentPhone { get; set; }

        [StringLength(50)]
        public string StudentEmail { get; set; }

        [StringLength(500)]
        public string StudentQuestion { get; set; }

        public virtual EVENT EVENT { get; set; }
    }
}
