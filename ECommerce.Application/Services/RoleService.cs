using AutoMapper;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ECommerce.Application.Services
{
    public class RoleService(IRoleRepository roleRepository, IRolePermissionService rolePermissionService, IMapper mapper)
        : BaseService<Role, RoleDTO, RoleVM>(roleRepository, mapper), IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IRolePermissionService _rolePermissionService = rolePermissionService;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<RoleVM>> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20)
        {
            // Define your filter, order by, and includes
            Expression<Func<Role, bool>> filter = null;
            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = q => q.OrderBy(p => p.Name);
            Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? includes = q => q.Include(p => p.RolePermissions)
                                                                                        .ThenInclude(p => p.Permission);

            var entities = await _roleRepository.GetFilteredPagedListAsync(
                select: x => _mapper.Map<RoleVM>(x),
                where: filter,
                orderBy: orderBy,
                include: includes,
                pageIndex,
                pageSize);

            return entities;
        }

        public async Task<RoleVM> GetRoleByIdAsync(int id)
        {
            // Define your filter, order by, and includes
            Expression<Func<Role, bool>> filter = q => q.Id == id;
            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = q => q.OrderBy(p => p.Name);
            Func<IQueryable<Role>, IIncludableQueryable<Role, object>>? includes = q => q.Include(p => p.RolePermissions)
                                                                                        .ThenInclude(p => p.Permission);

            var entities = await _roleRepository.GetFilteredFirstOrDefaultAsync(
                select: x => _mapper.Map<RoleVM>(x),
                where: filter,
                orderBy: orderBy,
                include: includes);

            return entities;
        }

        public async Task<RoleVM?> SaveRolePermission(RoleDTO roleDTO)
        {
            RoleDTO role = new()
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name
            };

            RoleVM roleVM = new();
            if (roleDTO.Id == 0)
                roleVM = await SaveAsync(role);
            else
                roleVM = await UpdateAsync(role);

            if (roleDTO.Permissions != null && roleDTO.Permissions.Any())
            {
                List<RolePermissionDTO> rolePermissionDTOs = new();
                foreach (var item in roleDTO.Permissions)
                {
                    rolePermissionDTOs.Add(new RolePermissionDTO
                    {
                        RoleId = roleVM.Id,
                        PermissionId = item.Id
                    });
                }

                await _rolePermissionService.DeleteRolePermissionAsync(roleVM.Id);

                await _rolePermissionService.SaveAllAsync(rolePermissionDTOs);
                // roleVM.Permissions = rolePermission.Select(x => x.Permission);

                await _rolePermissionService.DeleteRolePermissionAsync(roleVM.Id);

                await _rolePermissionService.SaveAllAsync(rolePermissionDTOs);

                RoleVM roleVVM = await GetRoleByIdAsync(roleVM.Id);
                roleVM.Permissions = roleVVM?.Permissions;
            }

            return roleVM;
        }
    }
}
