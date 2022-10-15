namespace JobFair.DomainModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EVENT")]
    public partial class EVENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENT()
        {
            STUDENT_EVENT = new HashSet<STUDENT_EVENT>();
        }

        [Key]
        public int IdEvent { get; set; }

        [StringLength(500)]
        public string EventName { get; set; }

        [StringLength(500)]
        public string EventDescription { get; set; }

        public DateTime? EventTime { get; set; }

        [StringLength(500)]
        public string EventLocation { get; set; }

        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_EVENT> STUDENT_EVENT { get; set; }
    }
}
