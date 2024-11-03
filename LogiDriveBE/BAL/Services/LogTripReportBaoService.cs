using System.Threading.Tasks;
using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class LogTripReportBaoService : ILogTripReportBao
    {
        private readonly ILogTripReportDao _reportDao;

        public LogTripReportBaoService(ILogTripReportDao reportDao)
        {
            _reportDao = reportDao;
        }

        public async Task<OperationResponse<byte[]>> GenerateLogTripPdfReportAsync()
        {
            var reportData = await _reportDao.GenerateLogTripPdfReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte PDF generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateLogTripCsvReportAsync()
        {
            var reportData = await _reportDao.GenerateLogTripCsvReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte CSV generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateLogTripExcelReportAsync()
        {
            var reportData = await _reportDao.GenerateLogTripExcelReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte Excel generado exitosamente", reportData);
        }
    }
}
