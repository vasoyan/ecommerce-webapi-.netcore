namespace ECommerce.Application.Models.DTOs;

public partial class CategoryDTO : BaseDTO
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    //public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
