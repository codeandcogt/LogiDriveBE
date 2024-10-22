using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class LogInspectionBaoService : ILogInspectionBao
    {
        private readonly ILogInspectionDao _logInspectionDao;

        public LogInspectionBaoService(ILogInspectionDao logInspectionDao)
        {
            _logInspectionDao = logInspectionDao;
        }

        public async Task<OperationResponse<LogInspectionDto>> CreateLogInspectionAsync(LogInspectionDto logInspectionDto)
        {
            return await _logInspectionDao.CreateLogInspectionAsync(logInspectionDto);
        }

        public async Task<OperationResponse<LogInspectionDto>> GetLogInspectionByIdAsync(int id)
        {
            return await _logInspectionDao.GetLogInspectionByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<LogInspectionDto>>> GetAllLogInspectionsAsync()
        {
            return await _logInspectionDao.GetAllLogInspectionsAsync();
        }

        public async Task<OperationResponse<LogInspectionDto>> UpdateLogInspectionAsync(int id, LogInspectionDto logInspectionDto)
        {
            return await _logInspectionDao.UpdateLogInspectionAsync(id, logInspectionDto);
        }

        public async Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id)
        {
            return await _logInspectionDao.DeleteLogInspectionAsync(id);
        }
    }
}
