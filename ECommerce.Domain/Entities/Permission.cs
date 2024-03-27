namespace ECommerce.Domain.Entities;

public partial class Permission : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
