using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.EntityTypeConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.OrderDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            builder.Property(e => e.PaymentMethod)
                .HasMaxLength(50);

            builder.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)");

            builder.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserId__4BAC3F29");

        }
    }
}
