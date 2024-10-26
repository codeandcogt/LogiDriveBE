using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Dao
{
    public interface ILogInspectionPartDao
    {
        Task<OperationResponse<LogInspectionPart>> CreateLogInspectionPartAsync(LogInspectionPart logInspectionPart);
        Task<OperationResponse<LogInspectionPart>> GetLogInspectionPartByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogInspectionPart>>> GetAllLogInspectionPartsAsync();
        Task<OperationResponse<LogInspectionPart>> UpdateLogInspectionPartAsync(LogInspectionPart logInspectionPart);
        Task<OperationResponse<bool>> DeleteLogInspectionPartAsync(int id);
    }
}
