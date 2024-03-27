namespace ECommerce.Domain.Entities;

public partial class ShoppingCart : BaseEntity
{
    public int? UserId { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public bool? Completed { get; set; }

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
}
