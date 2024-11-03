using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class ReportBaoService : IReportBao
    {
        private readonly IReportDao _reportDao;

        public ReportBaoService(IReportDao reportDao)
        {
            _reportDao = reportDao;
        }

        public async Task<OperationResponse<byte[]>> GenerateReportAsync(string reportType)
        {
            return await _reportDao.GenerateReportAsync(reportType);
        }
    }
}
