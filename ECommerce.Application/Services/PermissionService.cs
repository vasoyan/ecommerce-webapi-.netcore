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
    public class PermissionService(IPermissionRepository permissionRepository, IMapper mapper) 
        : BaseService<Permission, PermissionDTO, PermissionVM>(permissionRepository, mapper), IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository = permissionRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<PermissionVM>?> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20)
        {
            // Define your filter, order by, and includes
            Expression<Func<Permission, bool>> filter = null;
            Func<IQueryable<Permission>, IOrderedQueryable<Permission>> orderBy = q => q.OrderBy(p => p.Name);
            Func<IQueryable<Permission>, IIncludableQueryable<Permission, object>>? includes = null;

            var entities = await _permissionRepository.GetFilteredPagedListAsync(
                select: x => _mapper.Map<PermissionVM>(x),
                where: filter,
                orderBy: orderBy,
                include: includes,
                pageIndex,
                pageSize);

            return entities;
        }
    }
}
