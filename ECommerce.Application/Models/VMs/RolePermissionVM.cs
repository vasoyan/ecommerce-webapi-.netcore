namespace ECommerce.Application.Models.VMs;

public partial class RolePermissionVM : BaseViewModel
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual PermissionVM Permission { get; set; } = null!;

    public virtual RoleVM Role { get; set; } = null!;
}
