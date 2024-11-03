using System;

namespace LogiDriveBE.DAL.Models.DTO
{
    public class MaintenanceReportDto
    {
        public string VehicleBrand { get; set; }
        public string VehiclePlate { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public string MaintenanceType { get; set; }
        public string VehicleStatus { get; set; }
    }
}
