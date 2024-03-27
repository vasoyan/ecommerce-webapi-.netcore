namespace ECommerce.Domain.Entities;

public partial class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
