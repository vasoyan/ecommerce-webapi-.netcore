using System;
using System.Collections.Generic;

namespace ECommerce.API.Models;

public partial class RefreshToken
{
    public int UserId { get; set; }

    public string? Token { get; set; }

    public DateTime? ExpiryDateTime { get; set; }

    public virtual User User { get; set; } = null!;
}
