namespace LogiDriveBE.DAL.Models.DTO
{
    public class UserRolePermissionReportDto
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string PermissionName { get; set; }
        public DateTime PermissionCreationDate { get; set; }
    }
}
