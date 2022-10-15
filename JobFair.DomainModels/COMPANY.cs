namespace JobFair.DomainModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMPANY")]
    public partial class COMPANY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COMPANY()
        {
            COMPANY_JOB = new HashSet<COMPANY_JOB>();
            USERs = new HashSet<USER>();
        }

        [Key]
        public int IdCompany { get; set; }

        public int? ShowIndex { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [Column(TypeName = "ntext")]
        public string CompanyDescription { get; set; }

        [StringLength(500)]
        public string CompanyVideo { get; set; }

        [StringLength(255)]
        public string CompanyLink { get; set; }

        [StringLength(255)]
        public string MeetingLink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMPANY_JOB> COMPANY_JOB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER> USERs { get; set; }
    }
}
