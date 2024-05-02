using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.IServices
{
    public interface IRoleService : IBaseService<Role, RoleDTO, RoleVM>
    {
        Task<IEnumerable<RoleVM>> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20);

        Task<RoleVM> GetRoleByIdAsync(int id);

        Task<RoleVM?> SaveRolePermission(RoleDTO newRoleDTO, RoleVM? existingRoleVM = null);

        Task<bool> DeleteRoleAsync(int id);
    }
}
