using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using attr= Enterprise_E_Commerce_Management_System.Models.Attributes;
using Microsoft.VisualBasic;
using Enterprise_E_Commerce_Management_System.Models.Variants;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class AttributeConfiguration : IEntityTypeConfiguration<attr.Attribute>
    {
        public void Configure(EntityTypeBuilder<attr.Attribute> builder)
        {
            builder.Property(att => att.Name)
                   .HasConversion<byte>()
                   .HasComment(" Color = 1,Size = 2,Storage = 3,Material = 4,Weight = 5,Capacity" +
                   " = 6,Length = 7,Width = 8,Height = 9,Volume = 10,Brand = 11,Model = 12");

            builder.Property(att => att.DisplayType)
                .HasConversion<byte>()
                .HasComment("Text = 1,  Dropdown = 2, Color = 3,  Radio = 4,  Checkbox = 5");

        }
    }
}
