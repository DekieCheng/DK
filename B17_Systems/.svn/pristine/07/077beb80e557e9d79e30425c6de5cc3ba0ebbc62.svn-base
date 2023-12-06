using SDPSubSystem.Model.SysModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SDPSubSystem.Data.EntityMap
{
    public class SYS_UsageLogsMap : EntityTypeConfiguration<SYS_UsageLogs>
    {
        public SYS_UsageLogsMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Employee)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SysCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PageCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreateBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdateBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_UsageLogs");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Employee).HasColumnName("Employee");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.SysCode).HasColumnName("SysCode");
            this.Property(t => t.PageCode).HasColumnName("PageCode");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreationTime).HasColumnName("CreationTime");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
        }
    }
}
