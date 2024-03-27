using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class UserRepository(ECommerceDbContext eCommerceDb) : BaseRepository<User>(eCommerceDb), IUserRepository
    {
    }
}
