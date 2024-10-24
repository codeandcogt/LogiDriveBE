namespace LogiDriveBE.DAL.Models.DTO
{
    public class LogTripDto
    {
        public DateTime DateHour { get; set; }
        public string ActivityType { get; set; } = null!;
        public int? IdTracking { get; set; }
        public int IdVehicleAssignment { get; set; }
        public bool Status { get; set; }
    }
}
