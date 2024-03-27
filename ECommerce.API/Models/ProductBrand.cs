using System;
using System.Collections.Generic;

namespace ECommerce.API.Models;

public partial class ProductBrand
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
