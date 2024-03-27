using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.Infrastructure.Context;

namespace ECommerce.Infrastructure.Repositories
{
    public class RolePermissionRepository(ECommerceDbContext eCommerceDb) : BaseRepository<RolePermission>(eCommerceDb), IRolePermissionRepository
    {
    }
}
