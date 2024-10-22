using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class RoleDaoService : IRoleDao
    {
        private readonly LogiDriveDbContext _context;

        public RoleDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Role>> CreateRoleAsync(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                return new OperationResponse<Role>(200, "Role created successfully", role);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Role>(500, $"Error creating role: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Role>> GetRoleByIdAsync(int id)
        {
            try
            {
                var role = await _context.Roles
                    .Include(r => r.IdPermissions)
                    .FirstOrDefaultAsync(r => r.IdRole == id);

                if (role == null)
                {
                    return new OperationResponse<Role>(404, "Role not found");
                }

                return new OperationResponse<Role>(200, "Role retrieved successfully", role);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Role>(500, $"Error retrieving role: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Role>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _context.Roles
                    .Where(a => a.Status == true || a.Status == true)
                    .Include(r => r.IdPermissions)
                    .ToListAsync();

                return new OperationResponse<IEnumerable<Role>>(200, "Roles retrieved successfully", roles);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Role>>(500, $"Error retrieving roles: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Role>> UpdateRoleAsync(Role role)
        {
            try
            {
                _context.Entry(role).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResponse<Role>(200, "Role updated successfully", role);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Role>(500, $"Error updating role: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteRoleAsync(int id)
        {
            try
            {
                var role = await _context.Roles.FindAsync(id);
                if (role == null)
                {
                    return new OperationResponse<bool>(404, "Role not found");
                }

                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Role deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting role: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Role>> AssignPermissionsToRoleAsync(int roleId, IEnumerable<int> permissionIds)
        {
            try
            {
                var role = await _context.Roles
                    .Include(r => r.IdPermissions)
                    .FirstOrDefaultAsync(r => r.IdRole == roleId);

                if (role == null)
                {
                    return new OperationResponse<Role>(404, "Role not found");
                }

                role.IdPermissions.Clear();
                var permissions = await _context.Permissions
                    .Where(p => permissionIds.Contains(p.IdPermission))
                    .ToListAsync();

                role.IdPermissions = new List<Permission>(permissions);

                await _context.SaveChangesAsync();

                return new OperationResponse<Role>(200, "Permissions assigned successfully", role);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Role>(500, $"Error assigning permissions: {ex.Message}");
            }
        }
    }
}
