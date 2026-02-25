using Enterprise_E_Commerce_Management_System.Models.Reviews;
using Enterprise_E_Commerce_Management_System.Global.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Review_Rating",
                 $"[Rating] BETWEEN" +
                 $" {ValidationConstants.MinRate} " +
                 $"AND {ValidationConstants.MaxRate}"));

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Review_Comment",
                 $"LEN([Comment]) > {ValidationConstants.CommentMinLength}"));

            builder.Property(rv=>rv.Comment)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(rv=>rv.Product)
                .WithMany(prd=>prd.Reviews)
                .HasForeignKey(rv=>rv.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(rv => rv.Customer)
               .WithMany(cus => cus.Reviews)
               .HasForeignKey(rv => rv.CustomerId)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
