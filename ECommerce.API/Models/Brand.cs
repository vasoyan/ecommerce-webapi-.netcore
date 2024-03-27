using System;
using System.Collections.Generic;

namespace ECommerce.API.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ProductBrand> ProductBrands { get; set; } = new List<ProductBrand>();
}
