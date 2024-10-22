using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IPartVehicleBao
    {
        Task<OperationResponse<PartVehicle>> CreatePartVehicleAsync(PartVehicle partVehicle);
        Task<OperationResponse<PartVehicle>> GetPartVehicleByIdAsync(int id);
        Task<OperationResponse<IEnumerable<PartVehicle>>> GetAllPartVehiclesAsync();
        Task<OperationResponse<PartVehicle>> UpdatePartVehicleAsync(PartVehicle partVehicle);
        Task<OperationResponse<bool>> DeletePartVehicleAsync(int id);
        Task<OperationResponse<bool>> DeletePartVehicleStatusAsync(int id);
    }
}
