using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface ILogInspectionDao
    {
        Task<OperationResponse<LogInspectionDto>> CreateLogInspectionAsync(LogInspectionDto logInspectionDto);
        Task<OperationResponse<LogInspectionDto>> GetLogInspectionByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogInspectionDto>>> GetAllLogInspectionsAsync();
        Task<OperationResponse<LogInspectionDto>> UpdateLogInspectionAsync(int id, LogInspectionDto logInspectionDto);
        Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id);
        Task<OperationResponse<LogInspection>> GetLogInspectionByVehicleAssignmentAndTypeAsync(int idVehicleAssignment, string typeInspection);
    }
}
