using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class LogInspection
{
    public int IdLogInspection { get; set; }

    public int IdCollaborator { get; set; }

    public int? IdVehicleAssignment { get; set; }

    public int IdLogProcess { get; set; }

    public string Comment { get; set; } = null!;

    public string Odometer { get; set; } = null!;

    public string Fuel { get; set; } = null!;

    public string TypeInspection { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    public string? Image { get; set; }

    public virtual Collaborator IdCollaboratorNavigation { get; set; } = null!;

    public virtual LogProcess IdLogProcessNavigation { get; set; } = null!;

    public virtual VehicleAssignment? IdVehicleAssignmentNavigation { get; set; }

    public virtual ICollection<LogInspectionPart> LogInspectionParts { get; set; } = new List<LogInspectionPart>();

    public virtual ICollection<LogProcess> LogProcesses { get; set; } = new List<LogProcess>();
}
