using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductBrandRepository(ECommerceDbContext eCommerceDb) : BaseRepository<ProductBrand>(eCommerceDb), IProductBrandRepository
    {
    }
}
