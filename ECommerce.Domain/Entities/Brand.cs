namespace ECommerce.Domain.Entities;

public partial class Brand : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ProductBrand> ProductBrands { get; set; } = new List<ProductBrand>();
}
