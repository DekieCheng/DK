namespace SDPSubSystem.Model.SysModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_Icon : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Icon { get; set; }

        [StringLength(50)]
        public string Groups { get; set; }
    }
}
