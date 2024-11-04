namespace LogiDriveBE.DAL.Models.DTO
{
    public class CreateVehicleDto
    {
        public string Brand { get; set; } = null!;

        public string Plate { get; set; } = null!;

        public string Tyoe { get; set; } = null!;

        public string Year { get; set; } = null!;

        public string Mileage { get; set; } = null!;

        public int Capacity { get; set; }

        public string StatusVehicle { get; set; } = null!;

        public bool Status { get; set; }
    }
}
