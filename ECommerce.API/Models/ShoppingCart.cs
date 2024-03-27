using System;
using System.Collections.Generic;

namespace ECommerce.API.Models;

public partial class ShoppingCart
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public bool? Completed { get; set; }

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
}
