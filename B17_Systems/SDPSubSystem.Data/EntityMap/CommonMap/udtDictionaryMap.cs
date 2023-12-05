using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDPSubSystem.Model.AssistModels;

namespace SDPSubSystem.Data.EntityMap.CommonMap
{
    public class udtDictionaryMap:EntityTypeConfiguration<udtDictionary>
    {
        public udtDictionaryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DicText)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DicType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DicValue)
                .IsRequired()
                .HasMaxLength(150);

            //this.Property(t => t.Status);
                
            // Table & Column Mappings
            this.ToTable("udtDictionary");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DicText).HasColumnName("DicText");
            this.Property(t => t.DicType).HasColumnName("DicType");
            this.Property(t => t.DicValue).HasColumnName("DicValue");
            //this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
