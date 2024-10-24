using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface ILogTripBao
    {
        Task<OperationResponse<LogTrip>> CreateLogTripAsync(LogTrip logTrip);
        Task<OperationResponse<LogTrip>> UpdateLogTripAsync(LogTrip logTrip);
        Task<OperationResponse<bool>> DeleteLogTripAsync(int id);
        Task<OperationResponse<bool>> DeleteLogTripStatusAsync(int id);
        Task<OperationResponse<LogTrip>> GetLogTripByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogTrip>>> GetAllLogTripsAsync();
    }
}
