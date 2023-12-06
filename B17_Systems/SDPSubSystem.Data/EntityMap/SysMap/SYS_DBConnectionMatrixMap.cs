using SDPSubSystem.Model.SysModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SDPSubSystem.Data.EntityMap
{
    public class SYS_DBConnectionMatrixMap : EntityTypeConfiguration<SYS_DBConnectionMatrix>
    {
        public SYS_DBConnectionMatrixMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SiteCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Building)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MatrixName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreateBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdateBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_DBConnectionMatrix");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.SiteCode).HasColumnName("SiteCode");
            this.Property(t => t.Building).HasColumnName("Building");
            this.Property(t => t.MatrixName).HasColumnName("MatrixName");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreationTime).HasColumnName("CreationTime");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
        }
    }
}
