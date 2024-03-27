namespace ECommerce.Application.Models.VMs;

public partial class PermissionVM : BaseViewModel
{
    public string Name { get; set; } = null!;

    //public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
