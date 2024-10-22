using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface IVehicleDao
    {
        
        Task<OperationResponse<Vehicle>> CreateVehicleAsync(Vehicle vehicle);
        Task<OperationResponse<Vehicle>> GetVehicleByIdAsync(int id);
        Task<OperationResponse<IEnumerable<Vehicle>>> GetAllVehiclesAsync();
        Task<OperationResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle);
        Task<OperationResponse<bool>> DeleteVehicleAsync(int id);
        Task<OperationResponse<bool>> DeleteVehicleStatusAsync(int id);
    }
}
