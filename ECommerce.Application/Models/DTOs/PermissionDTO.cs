namespace ECommerce.Application.Models.DTOs;

public partial class PermissionDTO : BaseDTO
{
    public string Name { get; set; } = null!;

    //public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
