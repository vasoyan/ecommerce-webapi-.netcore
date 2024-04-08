namespace ECommerce.Application.Models.DTOs;

public partial class RolePermissionDTO : BaseDTO
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    // public virtual PermissionDTO Permission { get; set; } = null!;

    // public virtual RoleDTO Role { get; set; } = null!;
}
