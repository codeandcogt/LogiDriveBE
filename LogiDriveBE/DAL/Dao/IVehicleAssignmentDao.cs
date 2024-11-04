using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface IVehicleAssignmentDao
    {
        Task<OperationResponse<VehicleAssignmentDto>> CreateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto);
        Task<OperationResponse<VehicleAssigmentWithBrandDto>> GetVehicleAssignmentByIdAsync(int id);
        Task<OperationResponse<IEnumerable<VehicleAssignmentDto>>> GetAllVehicleAssignmentsAsync();
        Task<OperationResponse<VehicleAssignmentDto>> UpdateVehicleAssignmentAsync(VehicleAssignmentDto vehicleAssignmentDto);
        Task<OperationResponse<bool>> DeleteVehicleAssignmentAsync(int id);
        Task<OperationResponse<IEnumerable<VehicleAssignmentDto>>> GetVehicleAssignmentsByUserIdAsync(int userId);
        Task<OperationResponse<bool>> DeleteVehicleAssigmentStatusAsync(int id);
        Task<OperationResponse<IEnumerable<VehicleAssignmentView>>> GetVehicleAssignmentsByDateWithStatusUpdateAsync(DateTime specificDate);
        Task<OperationResponse<IEnumerable<VehicleAssignmentWithDetailsDto>>> GetVehicleAssignmentsByUserIdWithDetailsAsync(int userId, int hoursThreshold);
        Task<OperationResponse<VehicleAssignmentWithDetailsDto>> UpdateVehicleAssignmentStatusTripAsync(int id, bool statusTrip);
    }
}
