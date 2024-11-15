﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LogiDriveBE.DAL.Models;

public partial class Permission
{
    public int IdPermission { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Role> IdRoles { get; set; } = new List<Role>();
}
