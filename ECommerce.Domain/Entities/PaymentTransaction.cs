namespace ECommerce.Domain.Entities;

public partial class PaymentTransaction : BaseEntity
{
    public int? OrderId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDateTime { get; set; }

    public virtual Order? Order { get; set; }
}
