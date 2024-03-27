﻿namespace ECommerce.Domain.Entities;

public partial class RolePermission : BaseEntity
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
