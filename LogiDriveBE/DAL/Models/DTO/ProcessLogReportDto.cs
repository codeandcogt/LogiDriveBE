namespace LogiDriveBE.DAL.Models.DTO
{
    public class ProcessLogReportDto
    {
        public int IdLogProcess { get; set; }
        public string CollaboratorName { get; set; }
        public string Action { get; set; }
        public DateTime CreationDate { get; set; }
        public bool ReservationStatus { get; set; }
    }
}
