namespace LogiDriveBE.DAL.Models.DTO
{
    public class RoleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }
}
