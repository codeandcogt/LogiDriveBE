namespace LogiDriveBE.DAL.Models.DTO
{
    public class LogInspectionDto
    {
        public int IdLogInspection { get; set; }
        public int IdCollaborator { get; set; }
        public int IdVehicleAssignment { get; set; }
        public string Comment { get; set; }
        public string Odometer { get; set; }
        public string Fuel { get; set; }
        public string TypeInspection { get; set; } // Define si es entrega o recepción
        public string Image { get; set; }
        public bool Status { get; set; }
        public DateTime CreationDate { get; set; }
        public List<LogInspectionPartDto> PartsInspected { get; set; } = new List<LogInspectionPartDto>();
        public LogProcessDto LogProcess { get; set; } // Relación con LogProcess
    }
}
