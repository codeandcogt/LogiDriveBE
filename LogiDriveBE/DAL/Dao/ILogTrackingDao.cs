using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface ILogTrackingDao
    {
        Task<OperationResponse<LogTracking>> CreateLogTrackingAsync(LogTracking logTracking);
        Task<OperationResponse<LogTracking>> UpdateLogTrackingAsync(LogTracking logTracking);
        Task<OperationResponse<bool>> DeleteLogTrackingAsync(int id);
        Task<OperationResponse<bool>> DeleteLogTrackingStatusAsync(int id);
        Task<OperationResponse<LogTracking>> GetLogTrackingByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogTracking>>> GetAllLogTrackingsAsync();
    }
}
