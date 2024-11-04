using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class LogProcess
{
    public int IdLogProcess { get; set; }

    public int IdLogReservation { get; set; }

    public string Action { get; set; } = null!;

    public int IdCollaborator { get; set; }

    public int? IdVehicleAssignment { get; set; }

    public int? IdLogInspection { get; set; }

    public virtual Collaborator IdCollaboratorNavigation { get; set; } = null!;

    public virtual LogInspection? IdLogInspectionNavigation { get; set; }

    public virtual LogReservation IdLogReservationNavigation { get; set; } = null!;

    public virtual VehicleAssignment? IdVehicleAssignmentNavigation { get; set; }

    public virtual ICollection<LogInspection> LogInspections { get; set; } = new List<LogInspection>();
}
