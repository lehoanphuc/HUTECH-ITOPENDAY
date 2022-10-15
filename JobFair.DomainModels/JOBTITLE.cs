namespace JobFair.DomainModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JOBTITLE")]
    public partial class JOBTITLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JOBTITLE()
        {
            COMPANY_JOB = new HashSet<COMPANY_JOB>();
        }

        [Key]
        public int IdTitle { get; set; }

        [StringLength(200)]
        public string TitleName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMPANY_JOB> COMPANY_JOB { get; set; }
    }
}
