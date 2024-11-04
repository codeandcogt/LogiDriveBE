using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface ILogTrackingBao
    {
        Task<OperationResponse<LogTracking>> CreateLogTrackingAsync(TrakingDto trackingDto);
        Task<OperationResponse<LogTracking>> UpdateLogTrackingAsync(LogTracking logTracking);
        Task<OperationResponse<bool>> DeleteLogTrackingAsync(int id);

        Task<OperationResponse<bool>> DeleteLogTrackingStatusAsync(int id);
        Task<OperationResponse<IEnumerable<LogTracking>>> GetActiveTrackingByUserIdAsync(int userId);
        Task<OperationResponse<LogTracking>> GetLogTrackingByIdAsync(int id);
        Task<OperationResponse<LogTracking>> GetActiveLogTrackingByVehicleAssignmentIdAsync(int vehicleAssignmentId);
        Task<OperationResponse<IEnumerable<LogTracking>>> GetAllLogTrackingsAsync();

    }
}
