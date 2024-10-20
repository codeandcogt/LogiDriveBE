using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LogiDriveBE.DAL.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();


    public virtual ICollection<Permission> IdPermissions { get; set; } = new List<Permission>();
}
