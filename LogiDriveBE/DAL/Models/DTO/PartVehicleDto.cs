namespace LogiDriveBE.DAL.Models.DTO
{
    public class PartVehicleDto
    {

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string StatusPart { get; set; } = null!;

        public int IdVehicle { get; set; }

        public bool Status { get; set; }
    }
}
