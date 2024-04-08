using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.IServices
{
    public interface IRolePermissionService : IBaseService<RolePermission, RolePermissionDTO, RolePermissionVM>
    {
        Task<IEnumerable<RolePermissionVM>?> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20);
        Task<IEnumerable<RolePermissionVM>?> GetRolePermissionByRoleIdAsync(int roleId);
        Task<bool> DeleteRolePermissionAsync(int roleId);
    }
}
