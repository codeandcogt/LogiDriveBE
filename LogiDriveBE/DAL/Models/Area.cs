using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class Area
{
    public int IdArea { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ICollection<Collaborator> Collaborators { get; set; } = new List<Collaborator>();
}
