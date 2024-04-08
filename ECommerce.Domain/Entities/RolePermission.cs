using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities;

public partial class RolePermission : BaseEntity
{
    [Required]
    public int RoleId { get; set; }
    [Required]
    public int PermissionId { get; set; }

    [ForeignKey("PermissionId")]
    public virtual Permission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;
}
