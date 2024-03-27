namespace ECommerce.Domain.Entities;

public partial class RefreshToken : BaseEntity
{
    public int UserId { get; set; }

    public string? Token { get; set; }

    public DateTime? ExpiryDateTime { get; set; }

    public virtual User User { get; set; } = null!;
}
