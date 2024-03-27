namespace ECommerce.Application.Models.VMs;

public partial class RefreshTokenVM : BaseViewModel
{
    public int UserId { get; set; }

    public string? Token { get; set; }

    public DateTime? ExpiryDateTime { get; set; }

    public virtual UserVM User { get; set; } = null!;
}
