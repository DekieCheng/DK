namespace SDPSubSystem.Model.SysModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_Menu_Systems : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string CName { get; set; }

        public double SeqNo { get; set; }

        [Required]
        [StringLength(10)]
        public string IsVisuableToAll { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupCode { get; set; }

        [Required]
        [StringLength(20)]
        public string SysType { get; set; }

        [Required]
        [StringLength(500)]
        public string SystemUrl { get; set; }

        [StringLength(500)]
        public string HomeUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        [StringLength(100)]
        public string SysLogo { get; set; }

        [Required]
        [StringLength(10)]
        public string IsMatrix { get; set; }

        [StringLength(100)]
        public string MatrixCode { get; set; }

        [Required]
        [StringLength(50)]
        public string InitSiteCode { get; set; }

        [Required]
        [StringLength(50)]
        public string InitBuilding { get; set; }

        [StringLength(50)]
        public string Requestor { get; set; }

        [StringLength(50)]
        public string BizOwner { get; set; }

        [StringLength(50)]
        public string Developer { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RequestDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GoLiveDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}
