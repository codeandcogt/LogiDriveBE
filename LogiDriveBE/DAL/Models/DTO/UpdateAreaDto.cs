namespace LogiDriveBE.DAL.Models.DTO
{
    public class UpdateAreaDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
