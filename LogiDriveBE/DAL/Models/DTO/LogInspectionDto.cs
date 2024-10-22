namespace LogiDriveBE.DAL.Models.DTO
{
    public class LogInspectionDto
    {
        public int IdCollaborator { get; set; }
        public int? IdVehicleAssignment { get; set; }
        public string Comment { get; set; } = null!;
        public string Odometer { get; set; } = null!;
        public string Fuel { get; set; } = null!;
        public string TypeInspection { get; set; } = null!;
        public string? Image { get; set; }
        public bool Status { get; set; }
        public List<LogInspectionPartDto> PartsInspected { get; set; } = new List<LogInspectionPartDto>();
    }
}
