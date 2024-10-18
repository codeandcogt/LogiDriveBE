using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class DepartmentBaoService : IDepartmentBao
    {
        private readonly IDepartmentDao _departmentDao;

        public DepartmentBaoService(IDepartmentDao departmentDao)
        {
            _departmentDao = departmentDao;
        }

        public async Task<OperationResponse<Department>> CreateDepartmentAsync(Department department)
        {
            return await _departmentDao.CreateDepartmentAsync(department);
        }

        public async Task<OperationResponse<Department>> GetDepartmentByIdAsync(int id)
        {
            return await _departmentDao.GetDepartmentByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<Department>>> GetAllDepartmentsAsync()
        {
            return await _departmentDao.GetAllDepartmentsAsync();
        }

        public async Task<OperationResponse<Department>> UpdateDepartmentAsync(Department department)
        {
            return await _departmentDao.UpdateDepartmentAsync(department);
        }

        public async Task<OperationResponse<bool>> DeleteDepartmentAsync(int id)
        {
            return await _departmentDao.DeleteDepartmentAsync(id);
        }
    }
}
