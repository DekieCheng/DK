namespace SDPSubSystem.Model.SysModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_DBConnectionMatrix : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string SiteCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Building { get; set; }

        [Required]
        [StringLength(50)]
        public string MatrixName { get; set; }
    }
}
