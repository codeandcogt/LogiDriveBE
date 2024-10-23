﻿using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Dao
{
    public interface ILogInspectionDao
    {
        Task<OperationResponse<LogInspection>> CreateLogInspectionAsync(LogInspection logInspection);
        Task<OperationResponse<LogInspection>> GetLogInspectionByIdAsync(int id);
        Task<OperationResponse<IEnumerable<LogInspection>>> GetAllLogInspectionsAsync();
        Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id);
        Task<OperationResponse<LogInspection>> UpdateLogInspectionAsync(int id, LogInspection logInspection); // Cambiar aquí
    }

}