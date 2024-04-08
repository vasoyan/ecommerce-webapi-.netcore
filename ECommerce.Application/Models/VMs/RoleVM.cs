namespace ECommerce.Application.Models.VMs;

public partial class RoleVM : BaseViewModel
{
    public string Name { get; set; } = null!;

    public IEnumerable<PermissionVM>? Permissions { get; set; }
    //public IEnumerable<RolePermissionVM> RolePermissions { get; set; }

    // public virtual ICollection<UserVM> Users { get; set; } = new List<UserVM>();
}
