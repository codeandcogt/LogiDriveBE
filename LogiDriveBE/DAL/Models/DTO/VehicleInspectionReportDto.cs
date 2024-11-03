namespace LogiDriveBE.DAL.Models.DTO
{
    public class VehicleInspectionReportDto
    {
        public int InspectionID { get; set; }
        public int CollaboratorID { get; set; }
        public string CollaboratorName { get; set; }
        public string InspectionComment { get; set; }
        public DateTime InspectionDate { get; set; }
        public string InspectionType { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleStatus { get; set; }
    }
}
