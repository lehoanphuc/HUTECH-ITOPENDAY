namespace JobFair.DomainModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SETTING")]
    public partial class SETTING
    {
        [Key]
        [StringLength(100)]
        public string SettingKey { get; set; }

        [Column(TypeName = "ntext")]
        public string SettingValue { get; set; }
    }
}
