using System.Threading.Tasks;
using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class VehicleAvailabilityReportBaoService : IVehicleAvailabilityReportBao
    {
        private readonly IVehicleAvailabilityReportDao _reportDao;

        public VehicleAvailabilityReportBaoService(IVehicleAvailabilityReportDao reportDao)
        {
            _reportDao = reportDao;
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleAvailabilityPdfReportAsync()
        {
            var reportData = await _reportDao.GenerateVehicleAvailabilityPdfReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte PDF generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleAvailabilityCsvReportAsync()
        {
            var reportData = await _reportDao.GenerateVehicleAvailabilityCsvReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte CSV generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleAvailabilityExcelReportAsync()
        {
            var reportData = await _reportDao.GenerateVehicleAvailabilityExcelReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte Excel generado exitosamente", reportData);
        }
    }
}
