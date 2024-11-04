using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class LogTracking
{
    public int IdTracking { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public bool Status { get; set; } = true; // Nueva propiedad con valor predeterminado
    public int? IdLogTrip { get; set; } // Nueva propiedad opcional para la relación con LogTrip

    // Navegación hacia LogTrip
    public virtual LogTrip? LogTrip { get; set; }
}
