using System.Threading.Tasks;
using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class ProcessLogReportBaoService : IProcessLogReportBao
    {
        private readonly IProcessLogReportDao _reportDao;

        public ProcessLogReportBaoService(IProcessLogReportDao reportDao)
        {
            _reportDao = reportDao;
        }

        public async Task<OperationResponse<byte[]>> GenerateProcessLogPdfReportAsync()
        {
            var reportData = await _reportDao.GenerateProcessLogPdfReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte PDF generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateProcessLogCsvReportAsync()
        {
            var reportData = await _reportDao.GenerateProcessLogCsvReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte CSV generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateProcessLogExcelReportAsync()
        {
            var reportData = await _reportDao.GenerateProcessLogExcelReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte Excel generado exitosamente", reportData);
        }
    }
}
