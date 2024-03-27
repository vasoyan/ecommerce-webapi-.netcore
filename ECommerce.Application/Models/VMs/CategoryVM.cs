namespace ECommerce.Application.Models.VMs;

public partial class CategoryVM : BaseViewModel
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    //public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
