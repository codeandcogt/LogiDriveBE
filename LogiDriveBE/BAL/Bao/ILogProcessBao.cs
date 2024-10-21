using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface ILogProcessBao
    {
        Task<OperationResponse<LogProcessDto>> CreateLogProcessAsync(LogProcessDto logProcessDto);
        Task<OperationResponse<LogProcessDto>> GetLogProcessByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogProcessDto>>> GetAllLogProcessesAsync();
        Task<OperationResponse<LogProcessDto>> UpdateLogProcessAsync(LogProcessDto logProcessDto);
        Task<OperationResponse<bool>> DeleteLogProcessAsync(int id);
    }
}

