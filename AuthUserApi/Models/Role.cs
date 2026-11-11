using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuthUserApi.Models;

public partial class Role
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime? RegTime { get; set; }

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
