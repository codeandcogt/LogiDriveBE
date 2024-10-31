namespace LogiDriveBE.DAL.Models.DTO
{
    public class ActivityByCollaboratorReportDto
    {
        public string CollaboratorName { get; set; }
        public string CollaboratorLastName { get; set; }
        public string AreaName { get; set; }
        public string ActivityType { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string ActivityCategory { get; set; }
        public string InspectionType { get; set; }
        public string ReservationStatus { get; set; }
        public string TripType { get; set; }
        public int? ActivityStatus { get; set; }
    }
}
