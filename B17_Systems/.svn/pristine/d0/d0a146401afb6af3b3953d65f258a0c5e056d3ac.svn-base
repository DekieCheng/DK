using SDPSubSystem.Model.SysModels;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace SDPSubSystem.Data.EntityMap
{
    public class SYS_Menu_SystemsMap : EntityTypeConfiguration<SYS_Menu_Systems>
    {
        public SYS_Menu_SystemsMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IsVisuableToAll)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.GroupCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SysType)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SystemUrl)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Icon)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SysLogo)
                .HasMaxLength(100);

            this.Property(t => t.IsMatrix)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.MatrixCode)
                .HasMaxLength(100);

            this.Property(t => t.InitSiteCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.InitBuilding)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Requestor)
                .HasMaxLength(50);

            this.Property(t => t.BizOwner)
                .HasMaxLength(50);

            this.Property(t => t.Developer)
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.CreateBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdateBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_Menu_Systems");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CName).HasColumnName("CName");
            this.Property(t => t.SeqNo).HasColumnName("SeqNo");
            this.Property(t => t.IsVisuableToAll).HasColumnName("IsVisuableToAll");
            this.Property(t => t.GroupCode).HasColumnName("GroupCode");
            this.Property(t => t.SysType).HasColumnName("SysType");
            this.Property(t => t.SystemUrl).HasColumnName("SystemUrl");
            this.Property(t => t.Icon).HasColumnName("Icon");
            this.Property(t => t.SysLogo).HasColumnName("SysLogo");
            this.Property(t => t.IsMatrix).HasColumnName("IsMatrix");
            this.Property(t => t.MatrixCode).HasColumnName("MatrixCode");
            this.Property(t => t.InitSiteCode).HasColumnName("InitSiteCode");
            this.Property(t => t.InitBuilding).HasColumnName("InitBuilding");
            this.Property(t => t.Requestor).HasColumnName("Requestor");
            this.Property(t => t.BizOwner).HasColumnName("BizOwner");
            this.Property(t => t.Developer).HasColumnName("Developer");
            this.Property(t => t.RequestDate).HasColumnName("RequestDate");
            this.Property(t => t.GoLiveDate).HasColumnName("GoLiveDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreationTime).HasColumnName("CreationTime");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
        }
    }
}
