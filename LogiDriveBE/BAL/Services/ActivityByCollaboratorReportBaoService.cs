using System.Threading.Tasks;
using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class ActivityByCollaboratorReportBaoService : IActivityByCollaboratorReportBao
    {
        private readonly IActivityByCollaboratorReportDao _reportDao;

        public ActivityByCollaboratorReportBaoService(IActivityByCollaboratorReportDao reportDao)
        {
            _reportDao = reportDao;
        }

        public async Task<OperationResponse<byte[]>> GenerateActivityByCollaboratorPdfReportAsync()
        {
            var reportData = await _reportDao.GenerateActivityByCollaboratorPdfReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte PDF generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateActivityByCollaboratorCsvReportAsync()
        {
            var reportData = await _reportDao.GenerateActivityByCollaboratorCsvReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte CSV generado exitosamente", reportData);
        }

        public async Task<OperationResponse<byte[]>> GenerateActivityByCollaboratorExcelReportAsync()
        {
            var reportData = await _reportDao.GenerateActivityByCollaboratorExcelReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte Excel generado exitosamente", reportData);
        }
    }
}
