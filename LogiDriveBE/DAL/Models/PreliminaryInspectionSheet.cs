using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class PreliminaryInspectionSheet
{
    public int IdPreliminaryInspectionSheet { get; set; }

    public string Comment { get; set; } = null!;

    public int IdVehicleAssignment { get; set; }

    public bool Status { get; set; }

    public DateTime DateSheet { get; set; }

    public virtual VehicleAssignment IdVehicleAssignmentNavigation { get; set; } = null!;
}
