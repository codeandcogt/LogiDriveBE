using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class TownBaoService : ITownBao
    {
        private readonly ITownDao _townDao;

        public TownBaoService(ITownDao townDao)
        {
            _townDao = townDao;
        }

        public async Task<OperationResponse<TownDto>> CreateTownAsync(TownDto townDto)
        {
            return await _townDao.CreateTownAsync(townDto);
        }

        public async Task<OperationResponse<TownDto>> GetTownByIdAsync(int id)
        {
            return await _townDao.GetTownByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<TownDto>>> GetAllTownsAsync()
        {
            return await _townDao.GetAllTownsAsync();
        }

        public async Task<OperationResponse<TownDto>> UpdateTownAsync(TownDto townDto)
        {
            return await _townDao.UpdateTownAsync(townDto);
        }

        public async Task<OperationResponse<bool>> DeleteTownAsync(int id)
        {
            return await _townDao.DeleteTownAsync(id);
        }

        public async Task<OperationResponse<bool>> DeleteTownStatusAsync(int id)
        {
            return await _townDao.DeleteTownStatusAsync(id);
        }
    }
}
