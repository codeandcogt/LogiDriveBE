using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models;

public partial class TypeService
{
    public int IdTypeService { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
