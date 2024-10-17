using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IAreaBao
    {
        Task<OperationResponse<Area>> CreateAreaAsync(Area area);
        Task<OperationResponse<Area>> GetAreaByIdAsync(int id);
        Task<OperationResponse<IEnumerable<Area>>> GetAllAreasAsync();
        Task<OperationResponse<Area>> UpdateAreaAsync(Area area);
        Task<OperationResponse<bool>> DeleteAreaAsync(int id);
    }
}
