using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class Vehicle
{
    public int IdVehicle { get; set; }

    public string Brand { get; set; } = null!;

    public string Plate { get; set; } = null!;

    public string Tyoe { get; set; } = null!;

    public string Year { get; set; } = null!;

    public string Mileage { get; set; } = null!;

    public int Capacity { get; set; }

    public string StatusVehicle { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ICollection<PartVehicle> PartVehicles { get; set; } = new List<PartVehicle>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual ICollection<VehicleAssignment> VehicleAssignments { get; set; } = new List<VehicleAssignment>();
}
