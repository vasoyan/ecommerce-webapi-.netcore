using AutoMapper;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Org.BouncyCastle.Utilities;
using System.Linq.Expressions;

namespace ECommerce.Application.Services
{
    public class RolePermissionService(IRolePermissionRepository rolePermissionRepository, IMapper mapper)
        : BaseService<RolePermission, RolePermissionDTO, RolePermissionVM>(rolePermissionRepository, mapper), IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository = rolePermissionRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<RolePermissionVM>?> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20)
        {
            // Define your filter, order by, and includes
            Expression<Func<RolePermission, bool>> filter = null;
            Func<IQueryable<RolePermission>, IOrderedQueryable<RolePermission>> orderBy = q => q.OrderBy(p => p.Permission.Name);
            Func<IQueryable<RolePermission>, IIncludableQueryable<RolePermission, object>>? includes = q => q.Include(p => p.Role).Include(p => p.Permission);

            var entities = await _rolePermissionRepository.GetFilteredPagedListAsync(
                select: x => _mapper.Map<RolePermissionVM>(x),
                where: filter,
                orderBy: orderBy,
                include: includes,
                pageIndex,
                pageSize);

            return entities;
        }

        public async Task<IEnumerable<RolePermissionVM>?> GetRolePermissionByRoleIdAsync(int roleId)
        {
            Expression<Func<RolePermission, bool>> filter = x => x.RoleId == roleId;
            Func<IQueryable<RolePermission>, IOrderedQueryable<RolePermission>> orderBy = q => q.OrderBy(p => p.Permission.Name);
            Func<IQueryable<RolePermission>, IIncludableQueryable<RolePermission, object>>? includes = q => q.Include(p => p.Role)
                                                                                                             .Include(p => p.Permission);

            var entities = await _rolePermissionRepository.GetFilteredPagedListAsync(
                select: x => _mapper.Map<RolePermissionVM>(x),
                where: filter,
                orderBy: orderBy,
                include: includes);

            return entities;
        }

        public async Task<bool> DeleteRolePermissionAsync(int roleId)
        {
            IEnumerable<RolePermissionVM>? rolePermissions = await GetRolePermissionByRoleIdAsync(roleId);
            if (rolePermissions.Any())
                return await DeleteAllAsync(_mapper.Map<IEnumerable<RolePermissionDTO>>(rolePermissions));
            else
                return true;
        }
    }
}
