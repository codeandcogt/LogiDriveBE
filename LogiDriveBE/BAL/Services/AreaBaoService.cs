using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class AreaBaoService : IAreaBao
    {
        private readonly IAreaDao _areaDao;

        public AreaBaoService(IAreaDao areaDao)
        {
            _areaDao = areaDao;
        }

        public async Task<OperationResponse<Area>> CreateAreaAsync(Area area)
        {
            // Add any business logic here
            return await _areaDao.CreateAreaAsync(area);
        }

        public async Task<OperationResponse<Area>> GetAreaByIdAsync(int id)
        {
            return await _areaDao.GetAreaByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<Area>>> GetAllAreasAsync()
        {
            return await _areaDao.GetAllAreasAsync();
        }

        public async Task<OperationResponse<Area>> UpdateAreaAsync(Area area)
        {
            return await _areaDao.UpdateAreaAsync(area);
        }

        public async Task<OperationResponse<bool>> DeleteAreaAsync(int id)
        {
            return await _areaDao.DeleteAreaAsync(id);
        }
    }
}
