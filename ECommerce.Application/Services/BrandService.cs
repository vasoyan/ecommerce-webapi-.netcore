using AutoMapper;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ECommerce.Application.Services
{
    public class BrandService(IBrandRepository brandRepository, IMapper mapper)
        : BaseService<Brand, BrandDTO, BrandVM>(brandRepository, mapper), IBrandService
    {
        private readonly IBrandRepository _brandRepository = brandRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BrandVM>> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20)
        {
            // Define your filter, order by, and includes
            Expression<Func<Brand, bool>> filter = null;
            Func<IQueryable<Brand>, IOrderedQueryable<Brand>> orderBy = q => q.OrderBy(p => p.Name);
            Func<IQueryable<Brand>, IIncludableQueryable<Brand, object>>? includes = null;

            var entities = await _brandRepository.GetFilteredPagedListAsync(
                select: x => _mapper.Map<BrandVM>(x),
                where: filter,
                orderBy: orderBy,
                include: includes,
                pageIndex,
                pageSize);

            return entities;
        }



      
    }
}
