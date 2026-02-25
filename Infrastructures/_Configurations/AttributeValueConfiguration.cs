using Enterprise_E_Commerce_Management_System.Models.VariantAttributeValues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures._Configurations
{
    public class AttributeValueConfiguration : IEntityTypeConfiguration<AttributeValue>
    {
        public void Configure(EntityTypeBuilder<AttributeValue> builder)
        {
            builder.HasOne(val => val.Variant)
                .WithMany(v => v.AttributeValues)
                .HasForeignKey(val => val.VariantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(val => val.Attribute)
                .WithMany()
                .HasForeignKey(val => val.AttributeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(val => val.Value)
                .HasMaxLength(100);

            builder.HasKey(val => new { val.AttributeId, val.VariantId });
        }
    }
}
