using AutoMapper;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Linq.Expressions;

namespace ECommerce.Application.Services
{
    public class RoleService(IRoleRepository roleRepository,
                            IRolePermissionService rolePermissionService,
                            IPermissionRepository permissionRepository,
                            IMapper mapper)
        : BaseService<Role, RoleDTO, RoleVM>(roleRepository, mapper), IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IRolePermissionService _rolePermissionService = rolePermissionService;
        private readonly IPermissionRepository _permissionRepository = permissionRepository;
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

            var allPermissions = await _permissionRepository.GetAllAsync(); // Assuming a method to get all permissions

            List<RoleVM> roles = new List<RoleVM>();
            // Now, for each RoleVM, we need to adjust the Permissions property
            foreach (var role in entities)
            {
                // Create a dictionary to track which permissions are associated with the current role
                var rolePermissionIds = role.Permissions?.Select(p => p.Id).ToHashSet() ?? new HashSet<int>();

                // Map all permissions to PermissionVM and mark as checked based on role association
                role.Permissions = allPermissions.Select(p => new PermissionVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsChecked = rolePermissionIds.Contains(p.Id) // Check if this permission is associated with the role
                });
                roles.Add(role);
            }

            return roles;
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

            var allPermissions = await _permissionRepository.GetAllAsync(); // Assuming a method to get all permissions

            var rolePermissionIds = entities?.Permissions?.Select(p => p.Id).ToHashSet() ?? new HashSet<int>();

            entities.Permissions = allPermissions.Select(p => new PermissionVM
            {
                Id = p.Id,
                Name = p.Name,
                IsChecked = rolePermissionIds.Contains(p.Id) // Check if this permission is associated with the role
            });

            return entities;
        }

        public async Task<RoleVM?> SaveRolePermission(RoleDTO newRoleDTO, RoleVM? existingRoleVM = null)
        {
            // Create/Update New Role without Permission
            RoleDTO role = new()
            {
                Id = newRoleDTO.Id,
                Name = newRoleDTO.Name
            };

            RoleVM roleVM = new();
            if (newRoleDTO.Id == 0)
                roleVM = await SaveAsync(role);
            else
                roleVM = await UpdateAsync(role);

            // Delete All Permissions if existing
            if (existingRoleVM?.Permissions != null && existingRoleVM.Permissions.Any(x => x.IsChecked))
                await _rolePermissionService.DeleteRolePermissionAsync(roleVM.Id);

            // Save new Permissions 
            if (newRoleDTO.Permissions != null && newRoleDTO.Permissions.Any())
            {
                List<RolePermissionDTO> rolePermissionDTOs = new();
                foreach (var item in newRoleDTO.Permissions)
                {
                    rolePermissionDTOs.Add(new RolePermissionDTO
                    {
                        RoleId = roleVM.Id,
                        PermissionId = item.Id
                    });
                }

                await _rolePermissionService.SaveAllAsync(rolePermissionDTOs);

                RoleVM roleVVM = await GetRoleByIdAsync(roleVM.Id);
                roleVM.Permissions = roleVVM?.Permissions;
            }

            return roleVM;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var roleVM = await GetByIdAsync(id);
            if (roleVM != null)
            {
                await _rolePermissionService.DeleteRolePermissionAsync(roleVM.Id);

                await DeleteAsync(roleVM.Id);

                return true;
            }

            return false;
        }
    }
}
