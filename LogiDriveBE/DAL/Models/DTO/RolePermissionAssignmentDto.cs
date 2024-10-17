namespace LogiDriveBE.DAL.Models.DTO
{
    public class RolePermissionAssignmentDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
