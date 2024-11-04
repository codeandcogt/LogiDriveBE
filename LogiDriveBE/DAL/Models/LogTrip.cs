using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LogiDriveBE.DAL.Models;

public partial class LogTrip
{
    public int IdLogTrip { get; set; }
    public DateTime DateHour { get; set; }
    public string ActivityType { get; set; } = null!;
    public int? IdTracking { get; set; }
    public int IdVehicleAssignment { get; set; }
    public bool Status { get; set; }

    [JsonIgnore]
    public virtual LogTracking? IdTrackingNavigation { get; set; }
    [JsonIgnore]
    public virtual VehicleAssignment IdVehicleAssignmentNavigation { get; set; } = null!;
}
