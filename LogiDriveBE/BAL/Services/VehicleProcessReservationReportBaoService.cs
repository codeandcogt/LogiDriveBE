using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.UTILS;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Services
{
    public class VehicleProcessReservationReportBaoService : IVehicleProcessReservationReportBao
    {
        private readonly IVehicleProcessReservationReportDao _reportDao;

        public VehicleProcessReservationReportBaoService(IVehicleProcessReservationReportDao reportDao)
        {
            _reportDao = reportDao;
        }

        // Método para generar el reporte en PDF
        public async Task<OperationResponse<byte[]>> GenerateProcessReservationPdfReportAsync()
        {
            var reportData = await _reportDao.GenerateProcessReservationPdfReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte PDF generado exitosamente", reportData);
        }

        // Método para generar el reporte en CSV
        public async Task<OperationResponse<byte[]>> GenerateProcessReservationCsvReportAsync()
        {
            var reportData = await _reportDao.GenerateProcessReservationCsvReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte CSV generado exitosamente", reportData);
        }

        // Método para generar el reporte en Excel
        public async Task<OperationResponse<byte[]>> GenerateProcessReservationExcelReportAsync()
        {
            var reportData = await _reportDao.GenerateProcessReservationExcelReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte Excel generado exitosamente", reportData);
        }
    }
}
