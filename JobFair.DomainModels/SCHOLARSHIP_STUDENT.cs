namespace JobFair.DomainModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SCHOLARSHIP_STUDENT
    {
        [Key]
        public int IdReg { get; set; }

        [StringLength(15)]
        public string StudentCode { get; set; }

        [StringLength(100)]
        public string StudentName { get; set; }

        [StringLength(50)]
        public string StudentClass { get; set; }

        [StringLength(100)]
        public string StudentEmail { get; set; }

        [StringLength(10)]
        public string StudentPhone { get; set; }

        [StringLength(1000)]
        public string StudentActivities { get; set; }

        public short? StudentRole { get; set; }
        public string StudentRoles { get; set; }
        public double? StudentPoint { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime? TimeReg { get; set; }
    }
}
