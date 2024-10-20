using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IDepartmentBao
    {
        Task<OperationResponse<Department>> CreateDepartmentAsync(Department department);
        Task<OperationResponse<Department>> GetDepartmentByIdAsync(int id);
        Task<OperationResponse<IEnumerable<Department>>> GetAllDepartmentsAsync();
        Task<OperationResponse<Department>> UpdateDepartmentAsync(Department department);
        Task<OperationResponse<bool>> DeleteDepartmentAsync(int id);
    }
}
