using SDPSubSystem.Model.SysModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SDPSubSystem.Data.EntityMap
{
    public class SYS_IconMap : EntityTypeConfiguration<SYS_Icon>
    {
        public SYS_IconMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Icon)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Groups)
                .HasMaxLength(50);

            this.Property(t => t.CreateBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdateBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_Icon");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Icon).HasColumnName("Icon");
            this.Property(t => t.Groups).HasColumnName("Groups");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreationTime).HasColumnName("CreationTime");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
        }
    }
}
