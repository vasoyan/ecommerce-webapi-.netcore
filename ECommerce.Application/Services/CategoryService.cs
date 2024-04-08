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
    public class CategoryService(ICategoryRepository CategoryRepository, IMapper mapper)
        : BaseService<Category, CategoryDTO, CategoryVM>(CategoryRepository, mapper), ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository = CategoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CategoryVM>> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20)
        {
            // Define your filter, order by, and includes
            Expression<Func<Category, bool>> filter = null;
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = q => q.OrderBy(p => p.Name);
            Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? includes = null;

            var entities = await _CategoryRepository.GetFilteredPagedListAsync(
                select: x => _mapper.Map<CategoryVM>(x),
                where: filter,
                orderBy: orderBy,
                include: includes,
                pageIndex,
                pageSize);

            return entities;
        }



      
    }
}
