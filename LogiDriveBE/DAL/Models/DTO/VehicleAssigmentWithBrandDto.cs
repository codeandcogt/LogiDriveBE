namespace LogiDriveBE.DAL.Models.DTO
{
    public class VehicleAssigmentWithBrandDto
    {
        public int IdVehicleAssignment { get; set; }
        public string? Comment { get; set; }
        public string TripType { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdVehicle { get; set; }
        public int IdLogReservation { get; set; }
        public bool Status { get; set; }
        public DateTime CreationDate { get; set; }
        public bool StatusTrip { get; set; }
        public int DayQuantity { get; set; }
        public string Brand { get; set; }
        public string Plate { get; set; }
    }
}
