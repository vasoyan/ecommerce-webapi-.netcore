using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderItemRepository(ECommerceDbContext eCommerceDb) : BaseRepository<OrderItem>(eCommerceDb), IOrderItemRepository
    {
    }
}
