using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.OwnsOne(c => c.FullName,name=>
            {
                name.Property(n=>n.FirstName).HasMaxLength(50).IsRequired();
                name.Property(n => n.MiddleName).HasMaxLength(50).IsRequired(false);
                name.Property(n => n.LastName).HasMaxLength(50).IsRequired();
            });
            builder.OwnsOne(c => c.Address,address=>
            {
                address.Property(a => a.PostalCode).HasMaxLength(20);
                address.Property(a => a.Street).HasMaxLength(50);
                address.Property(a => a.City).HasMaxLength(50);

                address.HasOne(a=>a.Country)
                .WithMany()
                .HasForeignKey(a=>a.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            builder.HasMany(c => c.Carts)
                .WithOne(crt=>crt.Customer)
                .HasForeignKey(crt=>crt.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.OwnsOne(c => c.Address, add =>
            {
                add.Property(a => a.Street)
                   .HasMaxLength(200);

                add.Property(a => a.PostalCode)
                   .HasMaxLength(20)
                   .IsRequired(false);

                add.Property(a => a.CountryId)
                   .HasDefaultValue(29); // Egypt

                add.HasOne(a => a.Country)
                   .WithMany()
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Property(c => c.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            //builder.HasQueryFilter(c => c.IsActive);

         
        }
    }
}
