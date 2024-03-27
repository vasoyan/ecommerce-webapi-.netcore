using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductRepository(ECommerceDbContext eCommerceDb) : BaseRepository<Product>(eCommerceDb), IProductRepository
    {
    }
}
