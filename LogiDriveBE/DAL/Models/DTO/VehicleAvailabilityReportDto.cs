namespace LogiDriveBE.DAL.Models.DTO
{
    public class VehicleAvailabilityReportDto
    {
        public int IdVehicle { get; set; }
        public string? Brand { get; set; }              // Nullable por si hay valores NULL
        public string? Plate { get; set; }              // Nullable por si hay valores NULL
        public string? VehicleStatus { get; set; }      // Nullable por si hay valores NULL
        public DateTime? LastAssignmentEndDate { get; set; }
        public DateTime? LastInspectionDate { get; set; }
        public string? LastInspectionType { get; set; } // Nullable por si hay valores NULL
    }
}
