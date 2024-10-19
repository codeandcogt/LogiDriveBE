using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class RoleBaoService : IRoleBao
    {
        private readonly IRoleDao _roleDao;

        public RoleBaoService(IRoleDao roleDao)
        {
            _roleDao = roleDao;
        }

        public async Task<OperationResponse<Role>> CreateRoleAsync(Role role)
        {
            // You can add additional business logic here if needed
            return await _roleDao.CreateRoleAsync(role);
        }

        public async Task<OperationResponse<Role>> GetRoleByIdAsync(int id)
        {
            return await _roleDao.GetRoleByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<Role>>> GetAllRolesAsync()
        {
            return await _roleDao.GetAllRolesAsync();
        }

        public async Task<OperationResponse<Role>> UpdateRoleAsync(Role role)
        {
            // You can add additional business logic here if needed
            return await _roleDao.UpdateRoleAsync(role);
        }

        public async Task<OperationResponse<bool>> DeleteRoleAsync(int id)
        {
            return await _roleDao.DeleteRoleAsync(id);
        }

        public async Task<OperationResponse<Role>> AssignPermissionsToRoleAsync(int roleId, IEnumerable<int> permissionIds)
        {
            // You can add additional business logic here if needed
            return await _roleDao.AssignPermissionsToRoleAsync(roleId, permissionIds);
        }
    }
}

