namespace ECommerce.Domain.Entities;

public partial class ProductBrand : BaseEntity
{
    public int ProductId { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
