namespace ECommerce.Application.Models.DTOs;

public partial class RefreshTokenDTO : BaseDTO
{
    public int UserId { get; set; }

    public string? Token { get; set; }

    public DateTime? ExpiryDateTime { get; set; }

    public virtual UserDTO User { get; set; } = null!;
}
