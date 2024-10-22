using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.LogiDriveContext;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.Services
{
    public class DepartmentDaoService : IDepartmentDao
    {
        private readonly LogiDriveDbContext _context;

        public DepartmentDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Department>> CreateDepartmentAsync(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return new OperationResponse<Department>(200, "Department created successfully", department);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Department>(500, $"Error creating department: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Department>> GetDepartmentByIdAsync(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return new OperationResponse<Department>(404, "Department not found");
                }
                return new OperationResponse<Department>(200, "Department retrieved successfully", department);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Department>(500, $"Error retrieving department: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<Department>>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _context.Departments
                                                .Where(a => a.Status == true || a.Status == true)
                                                .ToListAsync();
                return new OperationResponse<IEnumerable<Department>>(200, "Departments retrieved successfully", departments);
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<Department>>(500, $"Error retrieving departments: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Department>> UpdateDepartmentAsync(Department department)
        {
            try
            {
                _context.Entry(department).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OperationResponse<Department>(200, "Department updated successfully", department);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Department>(500, $"Error updating department: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteDepartmentAsync(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return new OperationResponse<bool>(404, "Department not found");
                }

                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                return new OperationResponse<bool>(200, "Department deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting department: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> DeleteDepartamentStatusAsync(int id)
        {
            try
            {
                var departament = await _context.Departments.FindAsync(id);
                if (departament == null)
                {
                    return new OperationResponse<bool>(404, "Departament not found");
                }

                // Cambiar el estado en lugar de eliminar físicamente
                departament.Status = false;  
                await _context.SaveChangesAsync();

                return new OperationResponse<bool>(200, "Departament logically deleted (status set to false) successfully", true);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error deleting departament: {ex.Message}");
            }
        }
    }
}
