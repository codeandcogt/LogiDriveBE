﻿using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface IDepartmentDao
    {
        Task<OperationResponse<Department>> CreateDepartmentAsync(Department department);
        Task<OperationResponse<Department>> GetDepartmentByIdAsync(int id);
        Task<OperationResponse<IEnumerable<Department>>> GetAllDepartmentsAsync();
        Task<OperationResponse<Department>> UpdateDepartmentAsync(Department department);
        Task<OperationResponse<bool>> DeleteDepartmentAsync(int id);
        Task<OperationResponse<bool>> DeleteDepartamentStatusAsync(int id);
    }
}
