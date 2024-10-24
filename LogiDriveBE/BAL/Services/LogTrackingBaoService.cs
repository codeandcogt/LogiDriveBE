using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class LogTrackingBaoService : ILogTrackingBao
    {
        private readonly ILogTrackingDao _logTrackingDao;

        public LogTrackingBaoService(ILogTrackingDao logTrackingDao)
        {
            _logTrackingDao = logTrackingDao;
        }

        public async Task<OperationResponse<LogTracking>> CreateLogTrackingAsync(LogTracking logTracking)
        {
            return await _logTrackingDao.CreateLogTrackingAsync(logTracking);
        }

        public async Task<OperationResponse<LogTracking>> UpdateLogTrackingAsync(LogTracking logTracking)
        {
            return await _logTrackingDao.UpdateLogTrackingAsync(logTracking);
        }

        public async Task<OperationResponse<bool>> DeleteLogTrackingAsync(int id)
        {
            return await _logTrackingDao.DeleteLogTrackingAsync(id);
        }

        public async Task<OperationResponse<bool>> DeleteLogTrackingStatusAsync(int id)
        {
            return await _logTrackingDao.DeleteLogTrackingStatusAsync(id);
        }

        public async Task<OperationResponse<LogTracking>> GetLogTrackingByIdAsync(int id)
        {
            return await _logTrackingDao.GetLogTrackingByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<LogTracking>>> GetAllLogTrackingsAsync()
        {
            return await _logTrackingDao.GetAllLogTrackingsAsync();
        }
    }
}
