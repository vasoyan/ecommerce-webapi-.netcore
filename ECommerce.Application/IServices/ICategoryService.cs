using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.IServices
{
    public interface ICategoryService : IBaseService<Category, CategoryDTO, CategoryVM>
    {
        Task<IEnumerable<CategoryVM>> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20);

    }
}
