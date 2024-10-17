using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class LogInspectionPart
{
    public int IdLogInspectionPart { get; set; }

    public int IdLogInspection { get; set; }

    public int IdPartVehicle { get; set; }

    public string Comment { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime DateInspection { get; set; }

    public string? Image { get; set; }

    public virtual LogInspection IdLogInspectionNavigation { get; set; } = null!;

    public virtual PartVehicle IdPartVehicleNavigation { get; set; } = null!;
}
