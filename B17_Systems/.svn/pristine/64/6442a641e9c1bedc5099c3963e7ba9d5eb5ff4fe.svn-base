namespace SDPSubSystem.Model.SysModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_DBConnection : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string SiteCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Building { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string DBType { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string DBConn { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

    }
}
