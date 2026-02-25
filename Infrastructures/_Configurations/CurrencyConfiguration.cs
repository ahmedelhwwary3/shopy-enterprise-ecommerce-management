using Enterprise_E_Commerce_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Dapper.SqlMapper;

namespace Enterprise_E_Commerce_Management_System.Infrastructures._Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Currenci__3214EC07E9D9CA35");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.DollarRate)
                .HasColumnType("decimal(18, 6)");

            builder.HasOne(e => e.Country)
              .WithOne()
              .HasForeignKey<Currency>(cur=>cur.CountryId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
