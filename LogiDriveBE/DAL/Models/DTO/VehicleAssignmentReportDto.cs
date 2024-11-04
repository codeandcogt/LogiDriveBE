namespace LogiDriveBE.DAL.Models.DTO
{
    public class VehicleAssignmentReportDto
    {
        public int IdVehicleAssignment { get; set; }
        public string Comment { get; set; }
        public string TripType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string VehiclePlate { get; set; }
        public string CollaboratorName { get; set; }
    }
}
