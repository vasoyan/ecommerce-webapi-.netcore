using System;
using System.Collections.Generic;

namespace ECommerce.API.Models;

public partial class PaymentTransaction
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDateTime { get; set; }

    public virtual Order? Order { get; set; }
}
