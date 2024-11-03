using System.Threading.Tasks;
using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class MaintenanceReportBaoService : IMaintenanceReportBao
    {
        private readonly IMaintenanceReportDao _reportDao;

        public MaintenanceReportBaoService(IMaintenanceReportDao reportDao)
        {
            _reportDao = reportDao;
        }

        public async Task<OperationResponse<byte[]>> GenerateMaintenancePdfReportAsync()
        {
            var reportData = await _reportDao.GenerateMaintenancePdfReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte PDF generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateMaintenanceCsvReportAsync()
        {
            var reportData = await _reportDao.GenerateMaintenanceCsvReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte CSV generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateMaintenanceExcelReportAsync()
        {
            var reportData = await _reportDao.GenerateMaintenanceExcelReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte Excel generado exitosamente", reportData);
        }
    }
}
