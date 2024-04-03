using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.IServices
{
    public interface IPermissionService : IBaseService<Permission, PermissionDTO, PermissionVM>
    {
        Task<IEnumerable<PermissionVM>?> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20);
    }
}
