using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class Town
{
    public int IdTown { get; set; }

    public string Name { get; set; } = null!;

    public int IdDepartment { get; set; }

    public bool Status { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; } = null!;

    public virtual ICollection<LogReservation> LogReservations { get; set; } = new List<LogReservation>();
}
