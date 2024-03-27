using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductCategoryRepository(ECommerceDbContext eCommerceDb) : BaseRepository<ProductCategory>(eCommerceDb), IProductCategoryRepository
    {
    }
}
