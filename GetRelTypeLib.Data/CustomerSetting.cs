namespace GetRelTypeLib.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerSetting")]
    public partial class CustomerSetting
    {
        [Key]
        [StringLength(5)]
        public string CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Setting { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
