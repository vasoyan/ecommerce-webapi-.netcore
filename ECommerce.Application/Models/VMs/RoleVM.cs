namespace ECommerce.Application.Models.VMs;

public partial class RoleVM : BaseViewModel
{
    public string Name { get; set; } = null!;

    //public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    // public virtual ICollection<UserVM> Users { get; set; } = new List<UserVM>();
}
