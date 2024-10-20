using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface IVehicleAssignmentDao
    {
        Task<OperationResponse<VehicleAssignmentDto>> CreateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto);
        Task<OperationResponse<VehicleAssignmentDto>> GetVehicleAssignmentByIdAsync(int id);
        Task<OperationResponse<IEnumerable<VehicleAssignmentDto>>> GetAllVehicleAssignmentsAsync();
        Task<OperationResponse<VehicleAssignmentDto>> UpdateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto);
        Task<OperationResponse<bool>> DeleteVehicleAssignmentAsync(int id);
    }
}
