using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class ShoppingCartRepository(ECommerceDbContext eCommerceDb) : BaseRepository<ShoppingCart>(eCommerceDb), IShoppingCartRepository
    {
    }
}
