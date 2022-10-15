namespace JobFair.DomainModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USER")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            STUDENT_JOB = new HashSet<STUDENT_JOB>();
        }

        [Key]
        public int IdUser { get; set; }

        public int? IdCompany { get; set; }

        public int IdRole { get; set; }

        [StringLength(30)]
        public string Username { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        [StringLength(50)]
        public string ClassName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Token { get; set; }

        public virtual COMPANY COMPANY { get; set; }

        public virtual ROLE ROLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_JOB> STUDENT_JOB { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
