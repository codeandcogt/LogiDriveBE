namespace LogiDriveBE.DAL.Models.DTO
{
    public class TrakingDto
    {
        public int IdTracking { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool Status { get; set; } = true;
        public int? IdLogTrip { get; set; }
    }
}
