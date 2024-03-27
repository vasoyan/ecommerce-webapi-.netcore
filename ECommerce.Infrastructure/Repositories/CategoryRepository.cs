using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class CategoryRepository(ECommerceDbContext eCommerceDb) : BaseRepository<Category>(eCommerceDb), ICategoryRepository
    {
    }
}
