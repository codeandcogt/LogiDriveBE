using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models.DTO
{
    public class ServiceDto
    {
        public int? IdVehicle { get; set; }

        public string? Comment { get; set; }

        public string Maintenance { get; set; } = null!;

        public string NextServie { get; set; } = null!;

        public int? IdTypeService { get; set; }

        public bool Status { get; set; }

    }
}
