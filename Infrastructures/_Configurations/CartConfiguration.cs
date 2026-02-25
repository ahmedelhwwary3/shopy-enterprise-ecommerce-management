using Enterprise_E_Commerce_Management_System.Models.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class CartConfiguration:IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(c => c.Customer)
                .WithMany(cus=>cus.Carts)
                .HasForeignKey(c=>c.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

  
        }
    }
}
