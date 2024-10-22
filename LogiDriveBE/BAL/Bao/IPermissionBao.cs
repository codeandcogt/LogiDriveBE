using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Bao
{
    public interface IPermissionBao
    {
        Task<OperationResponse<Permission>> CreatePermissionAsync(Permission permission);
        Task<OperationResponse<Permission>> GetPermissionByIdAsync(int id);
        Task<OperationResponse<IEnumerable<Permission>>> GetAllPermissionsAsync();
        Task<OperationResponse<Permission>> UpdatePermissionAsync(Permission permission);
        Task<OperationResponse<bool>> DeletePermissionAsync(int id);
        Task<OperationResponse<bool>> DeletePermissionStatusAsync(int id);
    }
}
