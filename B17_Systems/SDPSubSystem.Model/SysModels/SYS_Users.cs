namespace SDPSubSystem.Model.SysModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SYS_Users : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string SiteCode { get; set; }

        [Required]
        [StringLength(50)]
        public string BuildingCode { get; set; }

        [Required]
        [StringLength(50)]
        public string UserType { get; set; }

        [StringLength(100)]
        public string EmpNO { get; set; }

        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountType { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? PWDLastChgDate { get; set; }

        public int ConFailLoginTimes { get; set; }

        public bool? NeedResetpwdFirstLogin { get; set; }

        [StringLength(50)]
        public string CName { get; set; }

        [StringLength(50)]
        public string EName { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Province { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(50)]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        public string OfficeNumber { get; set; }

        [StringLength(50)]
        public string MailAddress { get; set; }

        public DateTime? LastLoginTime { get; set; }

        [StringLength(50)]
        public string LastLoginIP { get; set; }

        public int LangID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ResignDate { get; set; }

        [StringLength(100)]
        public string ReportTo { get; set; }

        [StringLength(100)]
        public string AD { get; set; }

        [StringLength(100)]
        public string EmpClass { get; set; }

        [StringLength(500)]
        public string DeptL1 { get; set; }

        [StringLength(500)]
        public string DeptL2 { get; set; }

        [StringLength(500)]
        public string DeptL3 { get; set; }

        [StringLength(500)]
        public string CostCenter { get; set; }

        [StringLength(500)]
        public string CostCenterID { get; set; }

        public int? IsLock { get; set; }
    }
}
