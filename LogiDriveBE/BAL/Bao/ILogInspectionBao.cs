using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Bao
{
    public interface ILogInspectionBao
    {
        Task<OperationResponse<LogInspectionDto>> CreateLogInspectionAsync(LogInspectionDto logInspectionDto);
        Task<OperationResponse<LogInspectionDto>> GetLogInspectionByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogInspectionDto>>> GetAllLogInspectionsAsync();
        Task<OperationResponse<LogInspectionDto>> UpdateLogInspectionAsync(int id, LogInspectionDto logInspectionDto);
        Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id);
    }
}
