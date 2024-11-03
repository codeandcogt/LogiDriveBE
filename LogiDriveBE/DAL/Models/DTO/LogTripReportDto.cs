using System;

namespace LogiDriveBE.DAL.Models.DTO
{
    public class LogTripReportDto
    {
        public int IdLogTrip { get; set; }
        public DateTime TripDateHour { get; set; }
        public string ActivityType { get; set; }
        public string TripType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AssignmentComment { get; set; }
        public string VehicleBrand { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleType { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleMileage { get; set; }
        public int VehicleCapacity { get; set; }
        public string VehicleStatus { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
