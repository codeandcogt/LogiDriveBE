namespace LogiDriveBE.DAL.Models.DTO
{
    public class AppUserCollaboratorDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CollaboratorName { get; set; }
        public string CollaboratorLastName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public int IdRole { get; set; }
        public int IdArea { get; set; }
    }
}
