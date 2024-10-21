namespace LogiDriveBE.DAL.Models.DTO
{
    public class LogProcessDto
    {
        public int IdLogReservation { get; set; }
        public string Action { get; set; } = null!;
        public int IdCollaborator { get; set; }
        public int? IdVehicleAssignment { get; set; }
        public int? IdLogInspection { get; set; }
    }
}
