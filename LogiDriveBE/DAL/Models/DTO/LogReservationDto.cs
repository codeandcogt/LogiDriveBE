namespace LogiDriveBE.DAL.Models.DTO
{
    public class LogReservationDto
    {
        public int IdLogReservation { get; set; }
        public int? IdCollaborator { get; set; }
        public string Comment { get; set; } = null!;
        public int IdTown { get; set; }
        public int? NumberPeople { get; set; }
        public string StatusReservation { get; set; } = null!;
        public string? Justify { get; set; }
        public string Addres { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class UpdateStatusReservationDto
    {
        public string StatusReservation { get; set; } = null!;
        public string? Justify { get; set; }
    }
}
