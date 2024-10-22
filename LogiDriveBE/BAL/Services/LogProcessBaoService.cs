using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class LogProcessBaoService : ILogProcessBao
    {
        private readonly ILogProcessDao _logProcessDao;

        public LogProcessBaoService(ILogProcessDao logProcessDao)
        {
            _logProcessDao = logProcessDao;
        }

        public async Task<OperationResponse<LogProcessDto>> CreateLogProcessAsync(LogProcessDto logProcessDto)
        {
            return await _logProcessDao.CreateLogProcessAsync(logProcessDto);
        }

        public async Task<OperationResponse<LogProcessDto>> GetLogProcessByIdAsync(int id)
        {
            return await _logProcessDao.GetLogProcessByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<LogProcessDto>>> GetAllLogProcessesAsync()
        {
            return await _logProcessDao.GetAllLogProcessesAsync();
        }

        public async Task<OperationResponse<LogProcessDto>> UpdateLogProcessAsync(LogProcessDto logProcessDto)
        {
            return await _logProcessDao.UpdateLogProcessAsync(logProcessDto);
        }

        public async Task<OperationResponse<bool>> DeleteLogProcessAsync(int id)
        {
            return await _logProcessDao.DeleteLogProcessAsync(id);
        }

    }
}
