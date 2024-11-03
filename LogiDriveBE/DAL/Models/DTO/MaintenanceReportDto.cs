namespace LogiDriveBE.DAL.Models.DTO
{
    public class MaintenanceReportDto
    {
        public int IdVehicle { get; set; }
        public string MaintenanceType { get; set; }
        public string NextServiceDate { get; set; }
        public string ServiceComment { get; set; }
        public string TypeServiceName { get; set; }
        public DateTime? PartMaintenanceDate { get; set; }
        public string PartMaintenanceComment { get; set; }
    }
}
