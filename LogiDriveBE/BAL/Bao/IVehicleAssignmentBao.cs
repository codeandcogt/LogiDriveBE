using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IVehicleAssignmentBao
    {
        Task<OperationResponse<VehicleAssignmentDto>> CreateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto);
        Task<OperationResponse<VehicleAssignmentDto>> GetVehicleAssignmentByIdAsync(int id);
        Task<OperationResponse<IEnumerable<VehicleAssignmentDto>>> GetAllVehicleAssignmentsAsync();
        Task<OperationResponse<VehicleAssignmentDto>> UpdateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto);
        Task<OperationResponse<bool>> DeleteVehicleAssignmentAsync(int id);
        Task<OperationResponse<bool>> DeleteVehicleAssignmentStatusAsync(int id);
        Task<OperationResponse<IEnumerable<VehicleAssignmentDto>>> GetVehicleAssignmentsByUserIdAsync(int userId);
        Task<OperationResponse<IEnumerable<VehicleAssignmentView>>> GetVehicleAssignmentsByDateWithStatusUpdateAsync(DateTime specificDate);

    }
}
