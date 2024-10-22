using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IRoleBao
    {
        Task<OperationResponse<Role>> CreateRoleAsync(Role role);
        Task<OperationResponse<Role>> GetRoleByIdAsync(int id);
        Task<OperationResponse<IEnumerable<Role>>> GetAllRolesAsync();
        Task<OperationResponse<Role>> UpdateRoleAsync(Role role);
        Task<OperationResponse<bool>> DeleteRoleAsync(int id);
        Task<OperationResponse<Role>> AssignPermissionsToRoleAsync(int roleId, IEnumerable<int> permissionIds);
        Task<OperationResponse<bool>> DeleteRoleStatusAsync(int id);
    }
}
