using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface ITownBao
    {
        Task<OperationResponse<TownDto>> CreateTownAsync(TownDto townDto);
        Task<OperationResponse<TownDto>> GetTownByIdAsync(int id);
        Task<OperationResponse<IEnumerable<TownDto>>> GetAllTownsAsync();
        Task<OperationResponse<TownDto>> UpdateTownAsync(TownDto townDto);
        Task<OperationResponse<bool>> DeleteTownAsync(int id);
    }
}
