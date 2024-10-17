using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class Department
{
    public int IdDepartment { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Town> Towns { get; set; } = new List<Town>();
}
