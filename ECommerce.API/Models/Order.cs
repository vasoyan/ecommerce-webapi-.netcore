using System;
using System.Collections.Generic;

namespace ECommerce.API.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? ShippingAddress { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? OrderDateTime { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual User? User { get; set; }
}
