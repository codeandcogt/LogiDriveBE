namespace LogiDriveBE.DAL.Models.DTO
{
    public class VehiclePartDto
    {
        public int IdPartVehicle { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusPart { get; set; }
        public int IdVehicle { get; set; }
        public bool Status { get; set; }
    }
}
