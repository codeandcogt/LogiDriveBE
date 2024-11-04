using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class PermissionDaoService : IPermissionDao
    {
        private readonly LogiDriveDbContext _context;

        public PermissionDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Permission>> CreatePermissionAsync(Permission permission)
        {
            try
            {
                _context.Permissions.Add(permission);
                await _context.SaveChangesAsync();
                return new OperationResponse<Permission>(200, "Permission created successfully", permission);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Permission>(500, $"Error creating permission: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Permission>> GetPermissionByIdAsync(int id)
        {
            try
            {
                var permission = await _context.Permissions.FindAsync(id);
                if (permission == null)
                {
                    return new OperationResponse<Permission>(404, "Permission not found");
                }
                return new OperationResponse<Permission>(200, "Permission retrieved successfully", permission);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Permission>(500, $"Error retrieving permission: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Permission>>> GetAllPermissionsAsync()
        {
            try
            {
                var permissions = await _context.Permissions
                                                .Where(a => a.Status == true || a.Status == true)
                                                .ToListAsync();
                return new OperationResponse<IEnumerable<Permission>>(200, "Permissions retrieved successfully", permissions);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Permission>>(500, $"Error retrieving permissions: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Permission>> UpdatePermissionAsync(Permission permission)
        {
            try
            {
                _context.Entry(permission).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResponse<Permission>(200, "Permission updated successfully", permission);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Permission>(500, $"Error updating permission: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeletePermissionAsync(int id)
        {
            try
            {
                var permission = await _context.Permissions.FindAsync(id);
                if (permission == null)
                {
                    return new OperationResponse<bool>(404, "Permission not found");
                }

                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Permission deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting permission: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteLogPermissionStatusAsync(int id)
        {
            try
            {
                var permission = await _context.Permissions.FindAsync(id);
                if (permission == null)
                {
                    return new OperationResponse<bool>(404, "Permission not found");
                }

                // Cambiar el estado en lugar de eliminar físicamente
                permission.Status = false;
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "Permission logically deleted (status set to false) successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting Permission: {ex.Message}");
            }
        }
    }
}
