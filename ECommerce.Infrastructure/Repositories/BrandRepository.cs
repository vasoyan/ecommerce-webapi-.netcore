using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class BrandRepository(ECommerceDbContext eCommerceDb) : BaseRepository<Brand>(eCommerceDb), IBrandRepository
    {
    }
}
