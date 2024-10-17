using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class Service
{
    public int IdService { get; set; }

    public int? IdVehicle { get; set; }

    public string? Comment { get; set; }

    public string Maintenance { get; set; } = null!;

    public string NextServie { get; set; } = null!;

    public int? IdTypeService { get; set; }

    public bool Status { get; set; }

    public virtual TypeService? IdTypeServiceNavigation { get; set; }

    public virtual Vehicle? IdVehicleNavigation { get; set; }
}
