using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LogiDriveBE.DAL.Models;

public partial class AppUser
{
    public int IdAppUser { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdRole { get; set; }

    public bool Status { get; set; }

    [JsonIgnore]
    public virtual ICollection<Collaborator> Collaborators { get; set; } = new List<Collaborator>();

    [JsonIgnore]
    public virtual Role IdRoleNavigation { get; set; } = null!;
}
