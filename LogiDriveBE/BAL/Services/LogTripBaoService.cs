using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class LogTripBaoService : ILogTripBao
    {
        private readonly ILogTripDao _logTripDao;

        public LogTripBaoService(ILogTripDao logTripDao)
        {
            _logTripDao = logTripDao;
        }

        public async Task<OperationResponse<LogTrip>> CreateLogTripAsync(LogTrip logTrip)
        {
            // Add any business logic here if needed
            return await _logTripDao.CreateLogTripAsync(logTrip);
        }

        public async Task<OperationResponse<LogTrip>> UpdateLogTripAsync(LogTrip logTrip)
        {
            return await _logTripDao.UpdateLogTripAsync(logTrip);
        }

        public async Task<OperationResponse<bool>> DeleteLogTripAsync(int id)
        {
            return await _logTripDao.DeleteLogTripAsync(id);
        }

        public async Task<OperationResponse<bool>> DeleteLogTripStatusAsync(int id)
        {
            return await _logTripDao.DeleteLogTripStatusAsync(id);
        }

        public async Task<OperationResponse<LogTrip>> GetLogTripByIdAsync(int id)
        {
            return await _logTripDao.GetLogTripByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<LogTrip>>> GetAllLogTripsAsync()
        {
            return await _logTripDao.GetAllLogTripsAsync();
        }
    }
}
