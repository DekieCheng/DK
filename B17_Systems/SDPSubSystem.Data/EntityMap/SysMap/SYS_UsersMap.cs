using SDPSubSystem.Model.SysModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SDPSubSystem.Data.EntityMap
{
    public class SYS_UsersMap : EntityTypeConfiguration<SYS_Users>
    {
        public SYS_UsersMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.SiteCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BuildingCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpNO)
                .HasMaxLength(100);

            this.Property(t => t.Account)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AccountType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .HasMaxLength(50);

            this.Property(t => t.CName)
                .HasMaxLength(50);

            this.Property(t => t.EName)
                .HasMaxLength(50);

            this.Property(t => t.Country)
                .HasMaxLength(100);

            this.Property(t => t.Province)
                .HasMaxLength(100);

            this.Property(t => t.City)
                .HasMaxLength(200);

            this.Property(t => t.MobileNumber)
                .HasMaxLength(50);

            this.Property(t => t.OfficeNumber)
                .HasMaxLength(50);

            this.Property(t => t.MailAddress)
                .HasMaxLength(50);

            this.Property(t => t.LastLoginIP)
                .HasMaxLength(50);

            this.Property(t => t.ReportTo)
                .HasMaxLength(100);

            this.Property(t => t.AD)
                .HasMaxLength(100);

            this.Property(t => t.EmpClass)
                .HasMaxLength(100);

            this.Property(t => t.DeptL1)
                .HasMaxLength(500);

            this.Property(t => t.DeptL2)
                .HasMaxLength(500);

            this.Property(t => t.DeptL3)
                .HasMaxLength(500);

            this.Property(t => t.CostCenter)
                .HasMaxLength(500);

            this.Property(t => t.CostCenterID)
                .HasMaxLength(500);

            this.Property(t => t.CreateBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdateBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_Users");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.SiteCode).HasColumnName("SiteCode");
            this.Property(t => t.BuildingCode).HasColumnName("BuildingCode");
            this.Property(t => t.UserType).HasColumnName("UserType");
            this.Property(t => t.EmpNO).HasColumnName("EmpNO");
            this.Property(t => t.Account).HasColumnName("Account");
            this.Property(t => t.AccountType).HasColumnName("AccountType");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.PWDLastChgDate).HasColumnName("PWDLastChgDate");
            this.Property(t => t.ConFailLoginTimes).HasColumnName("ConFailLoginTimes");
            this.Property(t => t.NeedResetpwdFirstLogin).HasColumnName("NeedResetpwdFirstLogin");
            this.Property(t => t.CName).HasColumnName("CName");
            this.Property(t => t.EName).HasColumnName("EName");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Province).HasColumnName("Province");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.OfficeNumber).HasColumnName("OfficeNumber");
            this.Property(t => t.MailAddress).HasColumnName("MailAddress");
            this.Property(t => t.LastLoginTime).HasColumnName("LastLoginTime");
            this.Property(t => t.LastLoginIP).HasColumnName("LastLoginIP");
            this.Property(t => t.LangID).HasColumnName("LangID");
            this.Property(t => t.JoinDate).HasColumnName("JoinDate");
            this.Property(t => t.ResignDate).HasColumnName("ResignDate");
            this.Property(t => t.ReportTo).HasColumnName("ReportTo");
            this.Property(t => t.AD).HasColumnName("AD");
            this.Property(t => t.EmpClass).HasColumnName("EmpClass");
            this.Property(t => t.DeptL1).HasColumnName("DeptL1");
            this.Property(t => t.DeptL2).HasColumnName("DeptL2");
            this.Property(t => t.DeptL3).HasColumnName("DeptL3");
            this.Property(t => t.CostCenter).HasColumnName("CostCenter");
            this.Property(t => t.CostCenterID).HasColumnName("CostCenterID");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreationTime).HasColumnName("CreationTime");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.IsLock).HasColumnName("IsLock");
        }
    }
}
