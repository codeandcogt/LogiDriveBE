namespace LogiDriveBE.DAL.Models.DTO
{
    public class TownDto
    {
        public int IdTown { get; set; }
        public string Name { get; set; } = null!;
        public int IdDepartment { get; set; }
        public bool Status { get; set; }
    }
}
