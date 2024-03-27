using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class PaymentTransactionRepository(ECommerceDbContext eCommerceDb) : BaseRepository<PaymentTransaction>(eCommerceDb), IPaymentTransactionRepository
    {
    }
}
