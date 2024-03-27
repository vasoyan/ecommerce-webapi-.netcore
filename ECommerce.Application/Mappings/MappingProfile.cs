using AutoMapper;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BrandDTO, Brand>();
            CreateMap<Brand, BrandVM>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryVM>();

            CreateMap<PermissionDTO , Permission>();
            CreateMap<Permission, PermissionVM>();

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductVM>();

            CreateMap<RefreshTokenDTO, RefreshToken>();
            CreateMap<RefreshToken, RefreshTokenVM>();

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleVM>();

            CreateMap<UserDTO, User>();
            CreateMap<User, UserVM>();
        }
    }
}
