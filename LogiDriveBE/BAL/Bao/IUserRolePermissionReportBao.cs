using System.Threading.Tasks;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IUserRolePermissionReportBao
    {
        Task<OperationResponse<byte[]>> GenerateUserRolePermissionPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateUserRolePermissionCsvReportAsync();
        Task<OperationResponse<byte[]>> GenerateUserRolePermissionExcelReportAsync();
    }
}
