using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Models.DTOs;

public partial class RoleDTO : BaseDTO
{
    [Required]
    public string Name { get; set; } = null!;

    public IEnumerable<PermissionDTO>? Permissions { get; set; }

    // public virtual ICollection<UserDTO> Users { get; set; } = new List<UserDTO>();
}
