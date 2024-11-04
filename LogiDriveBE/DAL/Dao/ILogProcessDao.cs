using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Dao
{
    public interface ILogProcessDao
    {
        Task<OperationResponse<LogProcessDto>> CreateLogProcessAsync(LogProcessDto logProcessDto);
        Task<OperationResponse<LogProcessDto>> GetLogProcessByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogProcessDto>>> GetAllLogProcessesAsync();
        Task<OperationResponse<LogProcessDto>> UpdateLogProcessAsync(LogProcessDto logProcessDto);
        Task<OperationResponse<bool>> DeleteLogProcessAsync(int id);
    }
}
