namespace ECommerce.Application.Models.DTOs;

public partial class BrandDTO : BaseDTO
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    // public virtual ICollection<ProductBrand> ProductBrands { get; set; } = new List<ProductBrand>();
}
