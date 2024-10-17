using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class LogTracking
{
    public int IdTracking { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual ICollection<LogTrip> LogTrips { get; set; } = new List<LogTrip>();
}
