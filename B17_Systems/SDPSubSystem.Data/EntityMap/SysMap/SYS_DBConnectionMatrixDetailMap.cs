using SDPSubSystem.Model.SysModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SDPSubSystem.Data.EntityMap
{
    public class SYS_DBConnectionMatrixDetailMap : EntityTypeConfiguration<SYS_DBConnectionMatrixDetail>
    {
        public SYS_DBConnectionMatrixDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ConnCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MatrixCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreateBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdateBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_DBConnectionMatrixDetail");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ConnCode).HasColumnName("ConnCode");
            this.Property(t => t.MatrixCode).HasColumnName("MatrixCode");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreationTime).HasColumnName("CreationTime");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
        }
    }
}
