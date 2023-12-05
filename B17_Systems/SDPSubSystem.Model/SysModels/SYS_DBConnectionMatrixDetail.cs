namespace SDPSubSystem.Model.SysModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_DBConnectionMatrixDetail : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string ConnCode { get; set; }

        [Required]
        [StringLength(50)]
        public string MatrixCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Alias { get; set; }
    }
}
