using System.Threading.Tasks;
using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class UserRolePermissionReportBaoService : IUserRolePermissionReportBao
    {
        private readonly IUserRolePermissionReportDao _reportDao;

        public UserRolePermissionReportBaoService(IUserRolePermissionReportDao reportDao)
        {
            _reportDao = reportDao;
        }

        public async Task<OperationResponse<byte[]>> GenerateUserRolePermissionPdfReportAsync()
        {
            var reportData = await _reportDao.GenerateUserRolePermissionPdfReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte PDF generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateUserRolePermissionCsvReportAsync()
        {
            var reportData = await _reportDao.GenerateUserRolePermissionCsvReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte CSV generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateUserRolePermissionExcelReportAsync()
        {
            var reportData = await _reportDao.GenerateUserRolePermissionExcelReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte Excel generado exitosamente", reportData);
        }
    }
}
