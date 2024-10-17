using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class LogReservation
{
    public int IdLogReservation { get; set; }

    public int? IdCollaborator { get; set; }

    public string Comment { get; set; } = null!;

    public int IdTown { get; set; }

    public int? NumberPeople { get; set; }

    public string StatusReservation { get; set; } = null!;

    public string? Justify { get; set; }

    public string Addres { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual Collaborator? IdCollaboratorNavigation { get; set; }

    public virtual Town IdTownNavigation { get; set; } = null!;

    public virtual ICollection<LogProcess> LogProcesses { get; set; } = new List<LogProcess>();

    public virtual ICollection<VehicleAssignment> VehicleAssignments { get; set; } = new List<VehicleAssignment>();
}
