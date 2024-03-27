using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class RefreshTokenRepository(ECommerceDbContext eCommerceDb) : BaseRepository<RefreshToken>(eCommerceDb), IRefreshTokenRepository
    {
    }
}
