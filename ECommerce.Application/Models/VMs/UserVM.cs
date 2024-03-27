namespace ECommerce.Application.Models.VMs;

public partial class UserVM : BaseViewModel
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public string JwtToken { get; set; } = null!;

    //public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual RefreshTokenVM? RefreshToken { get; set; }

    public virtual RoleVM Role { get; set; } = null!;
}