using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models.DTO
{
    public class MaintenancePartDto
    {

        public string Comment { get; set; } = null!;

        public DateTime DateMaintenancePart { get; set; }

        public int? IdPartVehicle { get; set; }

        public bool Status { get; set; }
    }
}
