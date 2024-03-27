using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class PermissionRepository(ECommerceDbContext eCommerceDb) : BaseRepository<Permission>(eCommerceDb), IPermissionRepository
    {
    }
}
