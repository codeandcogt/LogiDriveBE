namespace LogiDriveBE.DAL.Models.DTO
{
    public class LogInspectionPartDto
    {
        public int IdLogInspectionPart { get; set; } // Agregada la propiedad
        public int IdPartVehicle { get; set; }
        public string Comment { get; set; } = null!;
        public bool Status { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Image { get; set; }


        public DateTime DateInspection { get; set; }
    }
}
