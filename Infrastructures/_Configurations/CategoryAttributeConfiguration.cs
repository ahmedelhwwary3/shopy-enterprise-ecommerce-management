using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using attr= Enterprise_E_Commerce_Management_System.Models.CategoryAttributes;
using Microsoft.VisualBasic;
using Enterprise_E_Commerce_Management_System.Models.Variants;
using System.ComponentModel;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class CategoryAttributeConfiguration : IEntityTypeConfiguration<attr.CategoryAttribute>
    {
        public void Configure(EntityTypeBuilder<attr.CategoryAttribute> builder)
        {
            builder.HasOne(ctAttr => ctAttr.Attribute)
                .WithMany()
                .HasForeignKey(ctAttr => ctAttr.AttributeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ctAttr => ctAttr.Category)
               .WithMany()
               .HasForeignKey(ctAttr => ctAttr.CategoryId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasKey(ctAttr => new { ctAttr.AttributeId, ctAttr.CategoryId }); 
        }
    }
}
