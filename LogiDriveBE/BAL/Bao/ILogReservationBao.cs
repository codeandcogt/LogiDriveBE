using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface ILogReservationBao
    {
        Task<OperationResponse<LogReservationDto>> CreateLogReservationAsync(LogReservationDto logReservationDto);
        Task<OperationResponse<LogReservationDto>> GetLogReservationByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogReservationDto>>> GetAllLogReservationsAsync();
        Task<OperationResponse<LogReservationDto>> UpdateLogReservationAsync(LogReservationDto logReservationDto);
        Task<OperationResponse<bool>> DeleteLogReservationAsync(int id);
        Task<OperationResponse<bool>> UpdateStatusReservationAsync(int id, UpdateStatusReservationDto updateStatusReservationDto);
        Task<OperationResponse<bool>> DeleteLogReservationStatusAsync(int id);
    }
}
