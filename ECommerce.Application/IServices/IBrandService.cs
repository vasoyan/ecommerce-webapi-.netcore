using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.IServices
{
    public interface IBrandService : IBaseService<Brand, BrandDTO, BrandVM>
    {
        Task<IEnumerable<BrandVM>> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20);

    }
}
