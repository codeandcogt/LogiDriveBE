namespace LogiDriveBE.DAL.Models.DTO
{
    public class PreliminaryInspectionSheetDto
    {
        public int IdPreliminaryInspectionSheet { get; set; }
        public string Comment { get; set; } = null!;
        public int IdVehicleAssignment { get; set; }
        public bool Status { get; set; }
        public DateTime DateSheet { get; set; }
    }
}
