using System;
using System.Collections.Generic;

namespace AuthUserApi.Models;

public partial class Role
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime? RegTime { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
