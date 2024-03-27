using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.EntityTypeConfigurations
{
    public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.ToTable("PaymentTransactions");

             builder.HasKey(e => e.Id);

             builder.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
             builder.Property(e => e.PaymentDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

             builder.HasOne(d => d.Order).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__PaymentTr__Order__534D60F1");

        }
    }
}
