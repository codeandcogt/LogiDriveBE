using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class PartVehicle
{
    public int IdPartVehicle { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string StatusPart { get; set; } = null!;

    public int IdVehicle { get; set; }

    public bool Status { get; set; }

    public virtual Vehicle IdVehicleNavigation { get; set; } = null!;

    public virtual ICollection<LogInspectionPart> LogInspectionParts { get; set; } = new List<LogInspectionPart>();

    public virtual ICollection<MaintenancePart> MaintenanceParts { get; set; } = new List<MaintenancePart>();
}
