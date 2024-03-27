using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class RoleRepository(ECommerceDbContext eCommerceDb) : BaseRepository<Role>(eCommerceDb), IRoleRepository
    {
    }
}
