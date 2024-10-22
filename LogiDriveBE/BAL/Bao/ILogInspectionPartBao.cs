using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Bao
{
    public interface ILogInspectionPartBao
    {
        Task<OperationResponse<LogInspectionPartDto>> CreateLogInspectionPartAsync(LogInspectionPartDto logInspectionPartDto);
        Task<OperationResponse<LogInspectionPartDto>> GetLogInspectionPartByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogInspectionPartDto>>> GetAllLogInspectionPartsAsync();
        Task<OperationResponse<LogInspectionPartDto>> UpdateLogInspectionPartAsync(int id, LogInspectionPartDto logInspectionPartDto);
        Task<OperationResponse<bool>> DeleteLogInspectionPartAsync(int id);
    }
}
