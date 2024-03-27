using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderRepository(ECommerceDbContext eCommerceDb) : BaseRepository<Order>(eCommerceDb), IOrderRepository
    {
    }
}
