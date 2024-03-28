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
    public class RoleService : BaseService<Role, RoleDTO, RoleVM>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper) : base(roleRepository, mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleVM>> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20)
        {
            // Define your filter, order by, and includes
            Expression<Func<Role, bool>> filter = null;
            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = q => q.OrderBy(p => p.Name);
            Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? includes = null;

            var entities = await _roleRepository.GetFilteredPagedListAsync(
                select: x => _mapper.Map<RoleVM>(x),
                where: filter,
                orderBy: orderBy,
                include: includes,
                pageIndex,
                pageSize);

            return entities;
        }
    }
}
