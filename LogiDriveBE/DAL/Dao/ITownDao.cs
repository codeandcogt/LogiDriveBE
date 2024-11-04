using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.DAL.Dao
{
    public interface ITownDao
    {
        Task<OperationResponse<TownDto>> CreateTownAsync(TownDto townDto);
        Task<OperationResponse<TownDto>> GetTownByIdAsync(int id);
        Task<OperationResponse<IEnumerable<TownDto>>> GetAllTownsAsync();
        Task<OperationResponse<TownDto>> UpdateTownAsync(TownDto townDto);
        Task<OperationResponse<bool>> DeleteTownAsync(int id);
        Task<OperationResponse<bool>> DeleteTownStatusAsync(int id);
        Task<OperationResponse<IEnumerable<TownDto>>> GetTownsByDepartmentIdAsync(int departmentId);
    }
}
