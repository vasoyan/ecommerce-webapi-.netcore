namespace ECommerce.Domain.Entities;

public partial class ShoppingCartItem : BaseEntity
{
    public int? CartId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual ShoppingCart? Cart { get; set; }

    public virtual Product? Product { get; set; }
}
