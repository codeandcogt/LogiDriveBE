using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class MaintenancePart
{
    public int IdMaintenancePart { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime DateMaintenancePart { get; set; }

    public int? IdPartVehicle { get; set; }

    public bool Status { get; set; }

    public virtual PartVehicle? IdPartVehicleNavigation { get; set; }
}
