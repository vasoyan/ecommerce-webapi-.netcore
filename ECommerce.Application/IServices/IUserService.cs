using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.IServices
{
    public interface IUserService : IBaseService<User, UserDTO, UserVM>
    {
        Task<IEnumerable<UserVM>> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20);
    }
}
