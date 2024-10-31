namespace LogiDriveBE.DAL.Models.DTO
{
    public class ProcessReservationReportDto
    {
        public int ReservationID { get; set; }
        public string ProcessAction { get; set; }
        public string CollaboratorName { get; set; }
        public int IdCollaborator { get; set; }
        public DateTime ProcessDate { get; set; }
        public string VehicleAssigned { get; set; }
    }
}
