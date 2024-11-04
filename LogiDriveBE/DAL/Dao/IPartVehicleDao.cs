using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;


namespace LogiDriveBE.DAL.Dao

{
    public interface IPartVehicleDao
    {
        Task<OperationResponse<PartVehicle>> CreatePartVehicleAsync(PartVehicle partVehicle);
        Task<OperationResponse<PartVehicle>> GetPartVehicleByIdAsync(int id);
        Task<OperationResponse<IEnumerable<PartVehicle>>> GetAllPartVehiclesAsync();
        Task<OperationResponse<PartVehicle>> UpdatePartVehicleAsync(PartVehicle partVehicle);
        Task<OperationResponse<bool>> DeletePartVehicleAsync(int id);
        Task<OperationResponse<bool>> DeleteLogPartVehicleStatusAsync(int id);
    }
}
