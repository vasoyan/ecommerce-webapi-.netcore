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

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductVM>();

            CreateMap<RefreshTokenDTO, RefreshToken>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenVM>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<Role, RoleVM>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(x => x.Permission).ToList()))
                .ReverseMap();

            CreateMap<PermissionDTO, Permission>();
            CreateMap<Permission, PermissionVM>();

            CreateMap<RolePermissionDTO, RolePermission>();
            CreateMap<RolePermission, RolePermissionVM>()
                 .ForMember(dest => dest.Permission, opt => opt.MapFrom(src => src.Permission))
                 .ReverseMap();
            CreateMap<RolePermissionVM, RolePermissionDTO>();

            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<UserVM, UserDTO>().ReverseMap();

        }
    }
}
