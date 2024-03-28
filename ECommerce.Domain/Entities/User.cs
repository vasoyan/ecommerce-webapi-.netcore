namespace ECommerce.Domain.Entities;

public partial class User : BaseEntity
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public string? JwtToken { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual RefreshToken? RefreshToken { get; set; }

    public virtual Role Role { get; set; } = null!;
}
