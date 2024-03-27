namespace ECommerce.Application.Models.VMs;

public partial class BrandVM : BaseViewModel
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    // public virtual ICollection<ProductBrand> ProductBrands { get; set; } = new List<ProductBrand>();
}
