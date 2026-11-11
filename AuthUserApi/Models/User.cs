using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuthUserApi.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
    public string? Salt { get; set; }

    public DateTime? RegTime { get; set; }

    [JsonIgnore]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
