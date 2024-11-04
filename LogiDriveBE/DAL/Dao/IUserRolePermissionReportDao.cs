using System.Collections.Generic;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Models.DTO;

namespace LogiDriveBE.DAL.Dao
{
    public interface IUserRolePermissionReportDao
    {
        Task<List<UserRolePermissionReportDto>> GetUserRolePermissionReportAsync();
        Task<byte[]> GenerateUserRolePermissionPdfReportAsync();
        Task<byte[]> GenerateUserRolePermissionCsvReportAsync();
        Task<byte[]> GenerateUserRolePermissionExcelReportAsync();
    }
}
