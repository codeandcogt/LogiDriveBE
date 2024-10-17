using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class Collaborator
{
    public int IdCollaborator { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? LicenseNumber { get; set; }

    public string? LicenseType { get; set; }

    public string Position { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int IdUser { get; set; }

    public int IdArea { get; set; }

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual Area IdAreaNavigation { get; set; } = null!;

    public virtual AppUser IdUserNavigation { get; set; } = null!;

    public virtual ICollection<LogInspection> LogInspections { get; set; } = new List<LogInspection>();

    public virtual ICollection<LogProcess> LogProcesses { get; set; } = new List<LogProcess>();

    public virtual ICollection<LogReservation> LogReservations { get; set; } = new List<LogReservation>();
}
