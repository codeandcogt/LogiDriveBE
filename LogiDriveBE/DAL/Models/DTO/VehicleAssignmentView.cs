namespace LogiDriveBE.DAL.Models.DTO
{
    public class VehicleAssignmentView
    {
        public DateTime DepartureTime { get; set; }
        public int IdVehicle { get; set; }
        public string Brand { get; set; }
        public string Plate { get; set; }
    }
}
