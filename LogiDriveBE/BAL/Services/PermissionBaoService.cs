using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class PermissionBaoService : IPermissionBao
    {
        private readonly IPermissionDao _permissionDao;

        public PermissionBaoService(IPermissionDao permissionDao)
        {
            _permissionDao = permissionDao;
        }

        public async Task<OperationResponse<Permission>> CreatePermissionAsync(Permission permission)
        {
            // You can add additional business logic here if needed
            return await _permissionDao.CreatePermissionAsync(permission);
        }

        public async Task<OperationResponse<Permission>> GetPermissionByIdAsync(int id)
        {
            return await _permissionDao.GetPermissionByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<Permission>>> GetAllPermissionsAsync()
        {
            return await _permissionDao.GetAllPermissionsAsync();
        }

        public async Task<OperationResponse<Permission>> UpdatePermissionAsync(Permission permission)
        {
            // You can add additional business logic here if needed
            return await _permissionDao.UpdatePermissionAsync(permission);
        }

        public async Task<OperationResponse<bool>> DeletePermissionAsync(int id)
        {
            return await _permissionDao.DeletePermissionAsync(id);
        }

        public async Task<OperationResponse<bool>> DeletePermissionStatusAsync(int id)
        {
            return await _permissionDao.DeleteLogPermissionStatusAsync(id);
        }
    }
}
