using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IMaintenancePartBao
    {
        Task<OperationResponse<bool>> SendToMaintenanceAsync(int idPartVehicle);

        Task<OperationResponse<MaintenancePart>> CreateMaintenancePartAsync(MaintenancePart maintenance);
        Task<OperationResponse<MaintenancePart>> GetMaintenancePartAsync(int id);
        Task<OperationResponse<IEnumerable<MaintenancePart>>> GetAllMaintenancePartAsync();
        Task<OperationResponse<MaintenancePart>> UpdateMaintenancePartAsync(MaintenancePart maintenance);
        Task<OperationResponse<bool>> DeleteMaintenancePartAsync(int id);
        Task<OperationResponse<bool>> SendPartToMaintenanceAsync(int partId);
        Task<OperationResponse<bool>> DeleteMaintenancePartStatusAsync(int id);
    }
}
