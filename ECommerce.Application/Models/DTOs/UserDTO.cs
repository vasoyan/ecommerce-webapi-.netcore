namespace ECommerce.Application.Models.DTOs;

public partial class UserDTO : BaseDTO
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public string JwtToken { get; set; } = null!;

    //public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual RefreshTokenDTO? RefreshToken { get; set; }

    public virtual RoleDTO Role { get; set; } = null!;
}
