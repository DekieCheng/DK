namespace SDPSubSystem.Model.SysModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_UsageLogs : BaseEntity
    {

        [Required]
        [StringLength(50)]
        public string Employee { get; set; }

        [Required]
        [StringLength(50)]
        public string IPAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string SysCode { get; set; }

        [Required]
        [StringLength(50)]
        public string PageCode { get; set; }
    }
}
